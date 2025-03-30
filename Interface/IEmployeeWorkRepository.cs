using EmployeeManagement.Models;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.Repository
{
    /// <summary>
    /// Interface for Class 
    /// </summary>
    public interface IEmployeeWorkRepository
    {
        IEnumerable<EmployeeWork> GetAllWork();
        IEnumerable<EmployeeWork> GetAllWorkByEmployee(int EmployeeId);
        EmployeeWork GetWork(int? Id);
        EmployeeWork Add(EmployeeWork user);
        EmployeeWork Update(EmployeeWork user);
        EmployeeWork UpdateStatus(int Id,string status);
        EmployeeWork Remove(int Id);
    }
}
