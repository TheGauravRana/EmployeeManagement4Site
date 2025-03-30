using EmployeeManagement.DTO;
using EmployeeManagement.Models;
using EmployeeManagement.Repository;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmployeeManagement.Controllers
{
    /// <summary>
    /// Default Screen for Admin and other roles are being redirected to Home/Details page as Employee dashboard
    /// </summary>
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _empRepo;
        private readonly IEmployeeWorkRepository _empWorkRepo;

        public HomeController(IEmployeeWorkRepository employeeWorkRepository, IEmployeeRepository employeeRepository)
        {
            _empRepo = employeeRepository;
            _empWorkRepo = employeeWorkRepository;
        }
        //Returns View with a Employee basic data list
        [Authorize]
        public ActionResult Index()
        {

            if (User.IsInRole("Admin"))
            {
                //data passed for partial view
                ViewBag.datalist = _empRepo.GetAllEmployee();
                ViewBag.ControllerName = "Home";
                return View();
            }
            
            return RedirectToAction("Details", "Home", new { Id = User.FindFirstValue(ClaimTypes.NameIdentifier) });
        }
        // To Register Employee
        [Authorize(Roles = "Admin")]
        public ViewResult RegEmployee()
        {

            return View();
        }
        // Save New Employee
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
                return View(employee);

            _empRepo.Add(employee);
            return RedirectToAction("Index","Home");
        }
        // Get Details of a particular Employee with id their ID / Confog Routing
        [Route("Details/{Id}")]
        [Authorize]
        public ViewResult Details(int Id)
        {
            var model = _empRepo.GetEmployee(Id);
            var _empVM = new EmployeeViewModel()
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email,
                Department = model.Department
            };
            ViewBag.todos = _empWorkRepo.GetAllWork();
            return View(_empVM);
        }
        //Edit Employee
        [Authorize]
        [HttpGet]
        public ViewResult Edit(int Id)
        {
            try
            {
                //get employee details
                var _empModel = _empRepo.GetEmployee(Id);
                return View(_empModel);
            }
            catch
            {
                return View(new Employee());
            }
        }
        //Update Employee
        [Authorize]
        [HttpPost]
        public IActionResult Edit(Employee employeeModel)
        {
            if (employeeModel.Id > 0 )
            {
                _empRepo.Update(employeeModel);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                _empRepo.Add(employeeModel);
                return RedirectToAction("Index","Home");
            }           

        }
        //Delete Employee
        [Authorize(Roles = "Admin")]
        public IActionResult Remove(int Id)
        {
            _empRepo.Remove(Id);
            return RedirectToAction("Index");
        }

        
    }
}
