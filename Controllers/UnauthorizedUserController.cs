using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    /// <summary>
    /// To handle all unauthorized user request
    /// </summary>
    public class UnauthorizedUserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
