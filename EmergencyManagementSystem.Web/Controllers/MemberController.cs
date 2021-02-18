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
    public class MemberController : Controller
    {
        private readonly IMemberRest _memberRest;
        private readonly IEmployeeRest _employeeRest;
        private readonly IAddressRest _addressRest;
        private readonly IVehicleRest _vehicleRest;
        public MemberController(IMemberRest memberRest, IEmployeeRest employeeRest, IAddressRest addressRest, IVehicleRest vehicleRest)
        {
            _memberRest = memberRest;
            _employeeRest = employeeRest;
            _addressRest = addressRest;
            _vehicleRest = vehicleRest;
        }


        public IActionResult Index(EmployeeFilter employeeFilter, VehicleFilter vehicleFilter, MemberFilter memberFilter)
        {

            ViewBag.Name = employeeFilter.Name;
            ViewBag.Id = employeeFilter.Id;
            ViewBag.Occupation = employeeFilter.Occupation;
            var employees = (PagedList<EmployeeModel>)_employeeRest.FindPaginated(employeeFilter).Where(d => d.Occupation != Occupation.RO & d.Occupation != Occupation.TARM).ToPagedList<EmployeeModel>();

            ViewBag.VehicleName = vehicleFilter.VehicleName;
            ViewBag.Plate = vehicleFilter.VehiclePlate;
            var vehicles = _vehicleRest.FindPaginated(vehicleFilter);

            ViewBag.VehicleId = memberFilter.VehicleId;
            var members = (PagedList<MemberModel>)_memberRest.FindPaginated(memberFilter).Where(d => d.EmployeeStatus == EmployeeStatus.Working).ToPagedList();

            //ViewBag.Guid = memberFilter.EmployeeGuid;
            //var employeeMembers = _employeeRest.FindPaginated(memberFilter);

            return View(new MemberRegisterModel { EmployeeModels = employees, VehicleModels = vehicles, MemberModels = members/*, EmployeeMembers = employeeMembers*/ });
        }

        [HttpPost]
        public IActionResult Register(MemberModel memberModel)
        {
            //está recebendo o EmployeeGuid e o VehicleId
            
            return View();
        }


    }
}
