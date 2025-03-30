using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Views.Shared.Components
{
    /// <summary>
    /// To handel User Login Success message
    /// </summary>
    public class LoginSuccessViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
