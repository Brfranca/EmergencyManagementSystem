using EmergencyManagementSystem.Service.Enums;
using EmergencyManagementSystem.Service.Filters;
using EmergencyManagementSystem.Service.Interfaces;
using EmergencyManagementSystem.Service.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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


        public IActionResult Index(int currentPage, Occupation occupation, long vehicleId)
        {
            ViewBag.OcccupationSelected = occupation;

            var membersWorking = _memberRest.FindAll(new MemberFilter { EmployeeStatus = EmployeeStatus.Working }).Model;

            var membersGuidWorking = membersWorking
                .Select(d => d.EmployeeGuid).Distinct()
                .ToList();

            var employees = _employeeRest.FindPaginated(new EmployeeFilter
            {
                Occupation = occupation,
                EmployeeGuidWorking = membersGuidWorking,
                IsMember = true,
                CurrentPage = currentPage == 0 ? 1 : currentPage
            });

            var vehicles = _vehicleRest.FindAll(new VehicleFilter());

            var currentVehicle = vehicles.Model.FirstOrDefault();
            if (vehicleId > 0)
                currentVehicle = vehicles.Model.FirstOrDefault(d => d.Id == vehicleId);

            ViewBag.VehicleId = currentVehicle.Id;

            List<EmployeeVehicleModel> employeeVehicles = new List<EmployeeVehicleModel>();

            foreach (var memberWorking in membersWorking.Where(d => d.VehicleId == currentVehicle.Id))
            {
                var employee = _employeeRest.Find(new EmployeeFilter { Guid = memberWorking.EmployeeGuid }).Model;
                if (employee != null)
                {
                    EmployeeVehicleModel employeeVehicleModel = new EmployeeVehicleModel
                    {
                        EmployeeModel = employee,
                        VehicleModel = currentVehicle,
                        MemberId = memberWorking.Id
                    };
                    employeeVehicles.Add(employeeVehicleModel);
                }
            }

            return View(new MemberRegisterModel { EmployeeModels = employees, VehicleModels = vehicles.Model, EmployeeVehicleModels = employeeVehicles });
        }


        public IActionResult Index2(MemberModel memberModel)
        {
            return View(new MemberRegisterModel());
        }


        [HttpPost]
        public IActionResult Register(MemberModel memberModel)
        {

            if (!ModelState.IsValid)
                return View(memberModel);

            memberModel.EmployeeStatus = EmployeeStatus.Working;
            memberModel.StartedWork = DateTime.Now;
            var result = _memberRest.Register(memberModel);
            if (!result.Success)
            {
                ViewBag.Error = result.Messages;
                return View(memberModel);
            }

            return RedirectToAction(nameof(Index), new { vehicleId = memberModel.VehicleId, occupation = memberModel.occupation });
        }


        public IActionResult Update(long id, Occupation occupation, long vehicleId)
        {
            if (!ModelState.IsValid)
                return View(nameof(Index));


            var result = _memberRest.Find(new MemberFilter { Id = id });
            if (!result.Success)
            {
                ViewBag.Error = result.Messages;
                var members = _employeeRest.FindPaginated(new MemberFilter());
                return View("Index", members);
            }


            result.Model.EmployeeStatus = EmployeeStatus.Finished;
            result.Model.FinishedWork = DateTime.Now;
            var resultUpdate = _memberRest.Update(result.Model);
            if (!result.Success)
            {
                ViewBag.Error = result.Messages;
                return View(nameof(Index));
            }

            return RedirectToAction(nameof(Index), new { vehicleId, occupation });
        }
    }
}
