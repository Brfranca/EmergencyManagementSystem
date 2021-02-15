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
        private readonly IAddressRest _addressRest;
        public EmployeeController(IEmployeeRest employeeRest, IAddressRest addressRest)
        {
            _employeeRest = employeeRest;
            _addressRest = addressRest;
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
            {
                ViewBag.Error = result.Messages;
                return View(employeeModel);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(long id)
        {
            var result = _employeeRest.Find(new EmployeeFilter { Id = id });
            if (!result.Success)
                return RedirectToAction(nameof(Index));

            return View(result.Model);
        }

        [HttpPost]
        public IActionResult Update(EmployeeModel employeeModel)
        {
            if (!ModelState.IsValid)
                return View(employeeModel);

            var result = _employeeRest.Update(employeeModel);
            if (!result.Success)
            {
                ViewBag.Error = result.Messages;
                return View(employeeModel);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var result = _employeeRest.Find(new EmployeeFilter { Id = id });

            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Index));

            if (!result.Success)
                return RedirectToAction(nameof(Index));
            return View(result.Model);
        }

        public IActionResult Delete(int id)
        {
            var result = _employeeRest.Delete(new EmployeeModel { Id = id });
            if (!result.Success)
                ModelState.AddModelError("Id", "Erro ao remover funcionário");

            return RedirectToAction(nameof(Index));
        }
    }
}
