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
    public class MockEmployeeWorkRepository : IEmployeeWorkRepository
    {
        private readonly string _filePath = Path.Combine("Data", "employeesWork.json");
        private readonly List<EmployeeWork> _empWork;

        public MockEmployeeWorkRepository()
        {
            //Chceck the .json file data to save or get the data form the file

            if (!File.Exists(_filePath))
            {
                //if file doesn't exist
                _empWork = new List<EmployeeWork>();
                SaveChanges();
            }
            else
            {
                //if file exist then get the data
                var jsonData = File.ReadAllText(_filePath);
                try
                {
                    _empWork = JsonSerializer.Deserialize<List<EmployeeWork>>(jsonData) ?? new List<EmployeeWork>();
                }
                catch
                {
                    _empWork = new List<EmployeeWork>();
                }
                
            }
        }

        //Get All Employee Work
        public IEnumerable<EmployeeWork> GetAllWork()
        {
            return _empWork;
        }
        // Get Work of Particular Employee with emp id
        public IEnumerable<EmployeeWork> GetAllWorkByEmployee(int employeeId)
        {
            return _empWork.Where(emp=>emp.WorkStatus != "Done" && emp.EmployeeId == employeeId);
        }
        //Get Work with ID
        public EmployeeWork GetWork(int? Id)
        {
            return _empWork.FirstOrDefault(emp => emp.Id == Id);
        }
        //Add New Work
        public EmployeeWork Add(EmployeeWork employeework)
        {
            try
            {
                employeework.Id = _empWork.Max(e => e.Id) + 1;
            }
            catch
            {
                employeework.Id = 1;
            }
            employeework.WorkStatus = "Active";
            _empWork.Add(employeework);
            SaveChanges();
            return employeework;
        }
        //Update the existing work
        public EmployeeWork Update(EmployeeWork employeeUpdate)
        {
            var emp = GetWork(employeeUpdate.Id);
            if (emp != null)
            {

                emp.EmployeeId = employeeUpdate.EmployeeId;
                emp.TodoTask = employeeUpdate.TodoTask;
                emp.Remarks = employeeUpdate.Remarks;
                emp.WorkStatus = employeeUpdate.WorkStatus;
                SaveChanges();
            }

            return emp;
        }
        //Update Work Status to done
        public EmployeeWork UpdateStatus(int wid, string status)
        {
            var emp = GetWork(wid);
            if (emp != null)
            {

                emp.WorkStatus = status;
                SaveChanges();
            }

            return emp;
        }
        //Delete the Work
        public EmployeeWork Remove(int Id)
        {
            var EmpRemove = GetWork(Id);

            if (EmpRemove != null)
            {
                _empWork.Remove(EmpRemove);
                SaveChanges();
            }

            return EmpRemove;
        }
        //After all operation Update the Json file employeeswork.json
        private void SaveChanges()
        {
            // Ensure the directory exists
            var directory = Path.GetDirectoryName(_filePath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            var jsonData = JsonSerializer.Serialize(_empWork, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, jsonData);
        }


    }
}
