using EmployeeManagement.Models;
using EmployeeManagement.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Claims;
using System.Data;

namespace EmployeeManagement.Controllers
{
    /// <summary>
    /// Handle Login and Logout Operations
    /// </summary>
    public class LoginController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmployeeRepository _emprepo;
        public LoginController(IUserRepository userRepository, IEmployeeRepository empRepo)
        {
            _userRepository = userRepository;
            _emprepo = empRepo;
        }
        public IActionResult Index()
        {
            return View();
        }
        //Login for both Admin and employee by using usertype
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password,string usertype)
        {
            var data = (dynamic)null;
            if(usertype == "Admin")
            data = _userRepository.GetUserByEmailAndPassword(email, password);
            if(usertype == "Employee")
            data = _emprepo.GetEmployeeByEmailAndPassword(email, password);

            if (data != null)
            {
                // Can also use Session to store userid for further authorization / HttpContext Used
                //HttpContext.Session.SetString("UserId", email);
                //HttpContext.Session.SetString("UserName", user.Name);

                // Create claims for cookie authentication
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, data.Id.ToString()),
                    new Claim(ClaimTypes.Name, data.Name),
                    new Claim(ClaimTypes.Email, data.Email),
                    new Claim(ClaimTypes.Role,usertype)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                // Sign in using the cookie authentication scheme
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    claimsPrincipal);


                if(usertype =="Admin")
                return RedirectToAction("Index", "Home");
                else
                return RedirectToAction("Details", "Home",new { Id = data.Id });
            }
            else
            {
                TempData["Error"] = "Invalid User Type or Email or Password!";
                return RedirectToAction("Index","Login");
            }
        }
        //Logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Sign out and clear the session
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }



    }
}
