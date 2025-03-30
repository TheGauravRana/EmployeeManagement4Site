using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EmployeeManagement.DTO;
using EmployeeManagement.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using static EmployeeManagement.Models.EmployeeWorkView;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace EmployeeManagement.Controllers
{

    /// <summary>
    /// Controller to Manage the Employee work releated Operations and reportings
    /// </summary>
    public class EmployeeWorkController : Controller
    {
        private readonly IEmployeeWorkRepository _empWork;
        private readonly IEmployeeRepository _empList;

        public EmployeeWorkController(IEmployeeWorkRepository employeeWorkRepository, IEmployeeRepository employee)
        {
            _empWork = employeeWorkRepository;
            _empList = employee;
        }
        
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            ViewBag.emplist = _empList.GetAllEmployee().Select(a=> new SelectListItem { Value = a.Id.ToString(),Text = a.Name}).ToList();
            return View(new EmployeeWork());
        }


        /// <summary>
        /// Save data with JSON request (AJAX)
        /// </summary>
        /// <param name="employeeWork"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public JsonResult SaveData(EmployeeWork employeeWork)
        {
            int rs = 0;
            if (!ModelState.IsValid)
                return Json(rs);

            _empWork.Add(employeeWork);
            rs = 1;
            return Json(rs);
        }
        //Edit Work
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ViewResult Edit(int?Id)
        {
            if (Id == null)
            {
                return View(new EmployeeWork());
            }
            else
            {
                var _workModel = _empWork.GetWork(Id);
                ViewBag.emplist = _empList.GetAllEmployee().Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).ToList();
                return View(_workModel);
            }
        }
        // Update Work
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(EmployeeWork empwork)
        {
            if (empwork.Id > 0 )
            {
                _empWork.Update(empwork);
                return RedirectToAction("Index", "EmployeeWork");
            }
            else
            {
                _empWork.Add(empwork);
                return RedirectToAction("Index", "EmployeeWork");
            }           
        }
        //Delete Work
        [Authorize(Roles = "Admin")]
        public IActionResult Remove(int Id)
        {
          _empWork.Remove(Id);
            return RedirectToAction("Index");
        }
        // Delete By JSON request
        [HttpPost]
        [Authorize(Roles ="Admin")]
        public JsonResult Delete(int Id)
        {
            int rs = 1;
            _empWork.Remove(Id);
            return Json(1);
        }
        // Update By JSON request
        [HttpPost]
        [Authorize]
        public JsonResult UpdateWorkStatus(int id)
        {
            int rs;
            try
            {
                _empWork.UpdateStatus(id, "Done");
                rs = 1;
            }
            catch
            {
                rs = 0;
            }
            return Json(rs);
        }
        // Get Work List of Single Employee By JSON request
        [HttpGet]
        [Authorize]
        public JsonResult GetEmpWorkList(int Id)
        {
            var data = _empWork.GetAllWorkByEmployee(Id);
                      
            return Json(data);
            
        }
        // Get Work List of All Employee By JSON request Only for Admin
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public JsonResult GetAllWork()
        {
            var employeeWorks = _empWork.GetAllWork();
            var employees = _empList.GetAllEmployee();
            var reportData = from work in employeeWorks
                             join emp in employees on work.EmployeeId equals emp.Id
                             select new EmployeeWorkView
                             {
                                 Id = work.Id,
                                 EmployeeName = emp.Name,
                                 EmployeeEmail = emp.Email,
                                 TodoTask = work.TodoTask,
                                 WorkStatus = work.WorkStatus,
                                 Remarks = work.Remarks
                             };

            return Json(reportData);
        }
        // Admin All Employee Work Report
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult Report()
        {
            var employeeWorks = _empWork.GetAllWork();
            var employees = _empList.GetAllEmployee();
            var reportData = from work in employeeWorks
                             join emp in employees on work.EmployeeId equals emp.Id
                             select new EmployeeWorkView
                             {
                                 Id= work.Id,
                                 EmployeeName = emp.Name,
                                 EmployeeEmail = emp.Email,
                                 TodoTask = work.TodoTask,
                                 WorkStatus = work.WorkStatus,
                                 Remarks = work.Remarks
                             };

            return View(reportData);
        }
        // Admin All Employee Work Report Export
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult ExportToXml()
        {
            var employeeWorks = _empWork.GetAllWork();
            var employees = _empList.GetAllEmployee();
            var reportData = from work in employeeWorks
                             join emp in employees on work.EmployeeId equals emp.Id
                             select new EmployeeWorkView
                             {
                                 Id = work.Id,
                                 EmployeeName = emp.Name,
                                 EmployeeEmail = emp.Email,
                                 TodoTask = work.TodoTask,
                                 WorkStatus = work.WorkStatus,
                                 Remarks = work.Remarks
                             };

            // Serialize the report data to XML
            var xmlSerializer = new XmlSerializer(typeof(List<EmployeeWorkView>));
            var xmlData = new StringBuilder();

            var settings = new System.Xml.XmlWriterSettings
            {
                OmitXmlDeclaration = true, 
                Indent = true 
            };

            using (var writer = System.Xml.XmlWriter.Create(xmlData, settings))
            {
                xmlSerializer.Serialize(writer, reportData.ToList());
            }

            var soapEnvelope = $@"<?xml version=""1.0"" encoding=""UTF-8""?>
        <soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
            <soap:Body>
                {xmlData}
            </soap:Body>
        </soap:Envelope>";

            return Content(soapEnvelope, "text/xml");
        }

    }
}
