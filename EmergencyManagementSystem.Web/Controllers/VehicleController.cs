using EmergencyManagementSystem.Service.Filters;
using EmergencyManagementSystem.Service.Interfaces;
using EmergencyManagementSystem.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmergencyManagementSystem.Web.Controllers
{
    [Authorize]
    public class VehicleController : Controller
    {
        private readonly IVehicleRest _vehicleRest;

        public VehicleController(IVehicleRest vehicleRest)
        {
            _vehicleRest = vehicleRest;
        }

        public IActionResult Index(VehicleFilter filter)
        {
            ViewBag.VehicleName = filter.VehicleName;
            ViewBag.Year = filter.Year;
            var employees = _vehicleRest.FindPaginated(filter);
            return View(employees);
        }


        public IActionResult Register()
        {
            return View(new VehicleModel());
        }

        [HttpPost]
        public IActionResult Register(VehicleModel vehicleModel)
        {
            vehicleModel.VehicleSituation = Service.Enums.VehicleSituation.Cleared;
            var result = _vehicleRest.Register(vehicleModel);
            if (!result.Success)
                return View(vehicleModel);

            return RedirectToAction("Index", "Vehicle");
        }

        public IActionResult Update(int id)
        {
            var result = _vehicleRest.Find(new VehicleFilter { Id = id });
            if (!result.Success)
                return RedirectToAction("Index");
            return View(result.Model);
        }

        [HttpPost]
        public IActionResult Update(VehicleModel vehicleModel)
        {
            var result = _vehicleRest.Update(vehicleModel);
            if (!result.Success)
                return View(vehicleModel);

            return RedirectToAction("Index", "Vehicle");
        }
    }
}
