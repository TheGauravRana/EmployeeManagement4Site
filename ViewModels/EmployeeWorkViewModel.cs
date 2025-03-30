using EmployeeManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.ViewModels
{
    /// <summary>
    /// View Model For Employee Report and List
    /// </summary>
    public class EmployeeWorkViewModel
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string TodoTask { get; set; }
        public string WorkStatus { get; set; }
        public string Remarks { get; set; }
    }
}
