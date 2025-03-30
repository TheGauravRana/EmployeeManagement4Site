using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    /// <summary>
    /// Created a Model to demonstrate model binding in a controller action
    /// </summary>
    public class EmployeeWork
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public int EmployeeId { get; set; }
        [Required(ErrorMessage ="This Field is Required")]
        public string TodoTask { get; set; }
        public string WorkStatus { get; set; }       
        public string Remarks { get; set; }       
       
    }

    /// <summary>
    /// View Model For Employee Work (we can also do this in ViewModel folder as we had created the other view Model but this is just for example)
    /// </summary>
    public class EmployeeWorkView
    {
            public int Id { get; set; }
            public string EmployeeName { get; set; }
            public string EmployeeEmail { get; set; }
            public string TodoTask { get; set; }
            public string WorkStatus { get; set; }
            public string Remarks { get; set; }

    }
}
