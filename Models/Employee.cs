using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EmployeeManagement.Models
{
    /// <summary>
    /// Created a Model to demonstrate model binding in a controller action
    /// </summary>
    public class Employee
    {
        public int Id { get; set; }       
        public string Name { get; set; }
        [Required]        
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [JsonIgnore]
        [Compare("Password", ErrorMessage ="Password and Confirm Password does not match")]
        public string ConfirmPassword { get; set; }
        [Required]
        public Dept? Department { get; set; } 
       
    }
}
