using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EmployeeManagement.DTO;
using EmployeeManagement.Repository;

namespace EmployeeManagement.Controllers
{
    /// <summary>
    /// Managing the Admin or managers to assign the tasks to the Employees used Im Memory (Hardcoded) user data. So all CRUD operations will be temp.
    /// </summary>
    /// Also Used Action Methods and action results View Results and View Data

    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        //Create and ViewResult for User 
        [Authorize(Roles ="Admin")]
        public ViewResult Index()
        {
            //data passed for partial view
            ViewBag.datalist = _userRepository.GetAllUser();
            ViewBag.ControllerName = "User";
            return View();
        }
        // Save New User (just for particular session)
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public ViewResult Index(UserModel userModel)
        {
            //return if the model state is invalid
            if (!ModelState.IsValid)
            {
                ViewBag.datalist = _userRepository.GetAllUser();
                ViewBag.ControllerName = "User";
                return View();
            }
            //save if model state is valid
            _userRepository.Add(userModel);

            //Used View Bag
            ViewBag.datalist = _userRepository.GetAllUser();
            ViewBag.ControllerName = "User";
            return View();
        }
        // Details of User 
        [Authorize(Roles = "Admin")]
        public ViewResult Details(int? Id)
        {
            var model = _userRepository.GetUser(Id);
            
            var _userVM = new UserViewModel()
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email,
                Password = model.Password
            };
            
            return View(_userVM);
        }
        //Edit User
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ViewResult Edit(int?Id)
        {
            if (Id == null)
            {
                return View(new UserModel());
            }
            else
            {
                //get user details
                var _userModel = _userRepository.GetUser(Id);
                return View(_userModel);
            }
        }
        //Update User / Actin Result
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(UserModel user)
        {
            if (user.Id > 0 )
            {
                _userRepository.Update(user);
                return RedirectToAction("Index", "User");
            }
            else
            {
                _userRepository.Add(user);
                return RedirectToAction("Index","User");
            }           
        }
        //Delete the user /Acton Method
        [Authorize(Roles = "Admin")]
        public IActionResult Remove(int Id)
        {
            _userRepository.Remove(Id);
            return RedirectToAction("Index");
        }

        
    }
}
