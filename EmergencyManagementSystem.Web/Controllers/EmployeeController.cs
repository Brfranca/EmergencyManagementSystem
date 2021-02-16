using EmergencyManagementSystem.Service.Enums;
using EmergencyManagementSystem.Service.Filters;
using EmergencyManagementSystem.Service.Interfaces;
using EmergencyManagementSystem.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EmergencyManagementSystem.Web.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRest _employeeRest;
        private readonly IAddressRest _addressRest;
        private readonly IUserRest _userRest;

        public EmployeeController(IEmployeeRest employeeRest, IAddressRest addressRest, IUserRest userRest)
        {
            _employeeRest = employeeRest;
            _addressRest = addressRest;
            _userRest = userRest;
        }

        public IActionResult Index(EmployeeFilter employeeFilter)
        {
            ViewBag.Name = employeeFilter.Name;
            ViewBag.CPF = employeeFilter.CPF;
            var employees = _employeeRest.FindPaginated(employeeFilter);
            return View(employees);
        }

        public IActionResult AddUser(long id)
        {
            var result = _userRest.Find(new UserFilter { EmployeeId = id });
            if (result.Model != null)
            {
                ViewBag.Error = new List<string> { "Esse funcionário já tem um usuário e senha cadastrado." };
                var employees = _employeeRest.FindPaginated(new EmployeeFilter());
                return View("Index", employees);
            }
            return RedirectToAction("Register", "User",new { employeeId = id});
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
            var result = _employeeRest.Find(new EmployeeFilter { Id = id });
            if (!result.Success)
            {
                ViewBag.Error = result.Messages;
                var employees = _employeeRest.FindPaginated(new EmployeeFilter());
                return View("Index", employees);
            }

            result.Model.Active = (result.Model.Active == Active.Active) ? Active.Disabled : Active.Active;
            var resultDisable = _employeeRest.Update(result.Model);
            if (!resultDisable.Success)
            {
                ViewBag.Error = new List<string> { "Erro ao remover o funcionário." };
                var employees = _employeeRest.FindPaginated(new EmployeeFilter());
                return View("Index", employees);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
