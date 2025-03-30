using EmployeeManagement.Models;
using EmployeeManagement.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmployeeManagement.DTO
{
    /// <summary>
    /// repo. for Contoleers to saperate the logic creation and to use as DI
    /// </summary>
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private readonly string _filePath = Path.Combine("Data", "employees.json");
        private readonly List<Employee> _empList;

        public MockEmployeeRepository()
        {
            //Chceck the .json file data to save or get the data form the file
            if (!File.Exists(_filePath))
            {
                //if file doesn't exist
                _empList = new List<Employee>();
                SaveChanges();
            }
            else
            {
                //if file exist then get the data
                var jsonData = File.ReadAllText(_filePath);
                try
                {
                    _empList = JsonSerializer.Deserialize<List<Employee>>(jsonData) ?? new List<Employee>();
                }
                catch
                {
                    _empList = new List<Employee>();
                }
                
            }
        }

        //Get All Employee
        public IEnumerable<Employee> GetAllEmployee()
        {
            return _empList;
        }
        //Get Employee to be used for login of Employee user type
        public Employee GetEmployeeByEmailAndPassword(string email, string password)
        {
            return _empList.FirstOrDefault(e =>e.Email.ToUpper() == email.ToUpper() && e.Password == password);
        }
        // Get Particular Employee with ID from the List
        public Employee GetEmployee(int Id)
        {
            return _empList.FirstOrDefault(emp => emp.Id == Id);
        }
        //Save new Employee
        public Employee Add(Employee employee)
        {
            var dup = _empList.FirstOrDefault(d => d.Email.ToUpper() == employee.Email.ToUpper());
            if (dup != null)
            {
                return employee;
            }
            try
            {
                employee.Id = _empList.Max(e => e.Id) + 1;
            }
            catch
            {
                employee.Id = 1;
            }

            _empList.Add(employee);
            SaveChanges();
            return employee;
        }
        // Update Employee
        public Employee Update(Employee employeeUpdate)
        {
            var emp = GetEmployee(employeeUpdate.Id);
            if (employeeUpdate != null)
            {
                var dup = _empList.FirstOrDefault(d => d.Email.ToUpper() == emp.Email.ToUpper() && d.Id != emp.Id);
                if (dup != null)
                {
                    return employeeUpdate;
                }

                emp.Name = employeeUpdate.Name;
                emp.Email = employeeUpdate.Email;
                emp.Department = employeeUpdate.Department;
                SaveChanges();
            }

            return emp;
        }
        //Delete Employee
        public Employee Remove(int Id)
        {
            var EmpRemove = GetEmployee(Id);

            if (EmpRemove != null)
            {
                _empList.Remove(EmpRemove);
                SaveChanges();
            }

            return EmpRemove;
        }
        // After performing All task to update the json file employees.json
        private void SaveChanges()
        {
            // Ensure the directory exists
            var directory = Path.GetDirectoryName(_filePath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            var jsonData = JsonSerializer.Serialize(_empList, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, jsonData);
        }


    }
}
