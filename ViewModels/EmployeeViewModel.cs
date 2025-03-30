using EmployeeManagement.Models;

namespace EmployeeManagement.ViewModels
{
    /// <summary>
    /// View Model for Employee Work Report or Lists
    /// </summary>
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Dept? Department { get; set; }
    }
}
