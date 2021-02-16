using EmergencyManagementSystem.Service.Filters;
using EmergencyManagementSystem.Service.Interfaces;
using EmergencyManagementSystem.Service.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace EmergencyManagementSystem.Web.Controllers
{
    public class TeamMemberController : Controller
    {
        private readonly ITeamMemberRest _teamMemberRest;
        private readonly IEmployeeRest _employeeRest;
        private readonly IAddressRest _addressRest;

        public TeamMemberController(ITeamMemberRest teamMemberRest, IEmployeeRest employeeRest, IAddressRest addressRest)
        {
            _teamMemberRest = teamMemberRest;
            _employeeRest = employeeRest;
            _addressRest = addressRest;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register(EmployeeFilter employeeFilter)
        {
            ViewBag.Name = employeeFilter.Name;
            ViewBag.Id = employeeFilter.Id;
            ViewBag.Occupation = employeeFilter.Occupation;
            var employees = _employeeRest.FindPaginated(employeeFilter);
            return View(new TeamMemberRegisterViewModel { employeeModels = employees });
        }
    }
}
