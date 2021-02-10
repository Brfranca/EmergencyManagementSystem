using EmergencyManagementSystem.Service.Filters;
using EmergencyManagementSystem.Service.Interfaces;
using EmergencyManagementSystem.Service.Models;
using EmergencyManagementSystem.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace EmergencyManagementSystem.Web.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRest _employeeRest;
        public EmployeeController(IEmployeeRest employeeRest)
        {
            _employeeRest = employeeRest;
        }

        public IActionResult Index(EmployeeFilter employeeFilter)
        {
            ViewBag.Name = employeeFilter.Name;
            ViewBag.CPF = employeeFilter.CPF;
            var employees = _employeeRest.FindPaginated(employeeFilter);
            return View(employees);
        }

        public IActionResult Register()
        {
            return View(new EmployeeModel());
        }

        [HttpPost]
        public IActionResult Register(EmployeeModel employeeModel)
        {
            if (!ModelState.IsValid)
                return View(employeeModel);

            var result = _employeeRest.Register(employeeModel);
            if (!result.Success)
                return View(employeeModel);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            var result = _employeeRest.Find(new EmployeeFilter { Id = id });
            if (!result.Success)
                return RedirectToAction("Index");
            return View(result.Model);
        }

        public IActionResult Detail(EmployeeModel employeeModel)
        {
            return View();
        }


    }
}
