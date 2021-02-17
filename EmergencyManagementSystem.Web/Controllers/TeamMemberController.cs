using EmergencyManagementSystem.Service.Enums;
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
        private readonly IVehicleRest _vehicleRest;
        public TeamMemberController(ITeamMemberRest teamMemberRest, IEmployeeRest employeeRest, IAddressRest addressRest, IVehicleRest vehicleRest)
        {
            _teamMemberRest = teamMemberRest;
            _employeeRest = employeeRest;
            _addressRest = addressRest;
            _vehicleRest = vehicleRest;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register(EmployeeFilter employeeFilter, VehicleFilter vehicleFilter)
        {
             
            ViewBag.Name = employeeFilter.Name;
            ViewBag.Id = employeeFilter.Id;
            ViewBag.Occupation = employeeFilter.Occupation;
            var employees = (PagedList<EmployeeModel>)_employeeRest.FindPaginated(employeeFilter).Where(d => d.Occupation != Occupation.RO & d.Occupation != Occupation.TARM).ToPagedList<EmployeeModel>();

            ViewBag.VehicleName = vehicleFilter.VehicleName;
            ViewBag.Plate = vehicleFilter.VehiclePlate;
            var vehicles = _vehicleRest.FindPaginated(vehicleFilter);

            return View(new TeamMemberRegisterViewModel { EmployeeModels = employees, VehicleModels = vehicles});
        }

        public IActionResult Teste()
        {
            return View();
        }
    }
}
