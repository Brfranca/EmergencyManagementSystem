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


        public IActionResult Index(EmployeeFilter employeeFilter, VehicleFilter vehicleFilter)
        {

            ViewBag.Id = employeeFilter.Id;
            ViewBag.Occupation = employeeFilter.Occupation;

            var membersWorking = _memberRest.FindAll(new MemberFilter { EmployeeStatus = EmployeeStatus.Working }).Model;

            var membersGuidWorking = membersWorking
                .Select(d => d.EmployeeGuid).Distinct()
                .ToList();

            var employees = _employeeRest.FindPaginated(new EmployeeFilter
            {
                Occupation = employeeFilter.Occupation,
                EmployeeGuidWorking = membersGuidWorking,
                IsMember = true
            });

            ViewBag.Id = vehicleFilter.Id;

            var vehicles = _vehicleRest.FindAll(new VehicleFilter());

            var currentVehicle = vehicles.Model.FirstOrDefault();

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
            //está recebendo o EmployeeGuid e o VehicleId

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

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Update(long id)
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

            return RedirectToAction(nameof(Index));
        }
    }
}
