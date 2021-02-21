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

            ViewBag.Name = employeeFilter.Name;
            ViewBag.Id = employeeFilter.Id;
            ViewBag.Occupation = employeeFilter.Occupation;
            var employees = (PagedList<EmployeeModel>)_employeeRest.FindPaginated(employeeFilter).Where(d => d.Occupation != Occupation.RO & d.Occupation != Occupation.TARM).ToPagedList<EmployeeModel>();

            ViewBag.VehicleName = vehicleFilter.VehicleName;
            ViewBag.Plate = vehicleFilter.VehiclePlate;
            ViewBag.Id = vehicleFilter.Id;
            var vehicles = _vehicleRest.FindPaginated(vehicleFilter);


            var members = _memberRest.FindPaginated(new MemberFilter { VehicleId = vehicleFilter.Id }).Where(d => d.EmployeeStatus == EmployeeStatus.Working).ToList();


            List<EmployeeVehicleModel> employeeVehicles = new List<EmployeeVehicleModel>();

            if (members != null)
            {
                foreach (var item in members)
                {
                    var employee = _employeeRest.Find(new EmployeeFilter { Guid = item.EmployeeGuid }).Model;
                    var vehicle = _vehicleRest.Find(new VehicleFilter { Id = item.VehicleId }).Model;
                    if (employee != null && vehicle != null)
                    {
                        EmployeeVehicleModel employeeVehicleModel = new EmployeeVehicleModel
                        {
                            EmployeeModel = employee,
                            VehicleModel = vehicle,
                            MemberId = item.Id
                        };

                        employeeVehicles.Add(employeeVehicleModel);
                    }
                }
            }

            return View(new MemberRegisterModel { EmployeeModels = employees, VehicleModels = vehicles, EmployeeVehicleModels = employeeVehicles});
        }


        public IActionResult Teste(MemberModel memberModel)
        {
            return View(new MemberModel());
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
