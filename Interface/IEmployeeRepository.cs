using EmployeeManagement.Models;
using System.Collections;
using System.Collections.Generic;


namespace EmployeeManagement.Repository
{
    /// <summary>
    /// Interface for Class 
    /// </summary>
    public interface IEmployeeRepository
    {
        Employee GetEmployeeByEmailAndPassword(string email, string password);
        IEnumerable<Employee> GetAllEmployee();
        Employee GetEmployee(int id);
        Employee Add(Employee employee);
        Employee Update(Employee employee);
        Employee Remove(int id);
    }
}
