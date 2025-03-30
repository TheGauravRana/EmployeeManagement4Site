using EmployeeManagement.Models;

namespace EmployeeManagement.ViewModels
{
    /// <summary>
    /// User Model for report and list
    /// </summary>
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get;set; }
    }
}
