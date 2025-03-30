using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{

    /// <summary>
    /// Created a Model to demonstrate model binding in a controller action
    /// </summary>
    public class UserModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
