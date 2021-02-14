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
            if (!ModelState.IsValid)
                return View(vehicleModel);

            vehicleModel.VehicleStatus = Service.Enums.VehicleStatus.Cleared;
            var result = _vehicleRest.Register(vehicleModel);
            if (!result.Success)
                return View(vehicleModel);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {

            var result = _vehicleRest.Find(new VehicleFilter { Id = id });

            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Index));

            if (!result.Success)
                return RedirectToAction(nameof(Index));
            return View(result.Model);
        }

        [HttpPost]
        public IActionResult Update(VehicleModel vehicleModel)
        {
            if (!ModelState.IsValid)
                return View(vehicleModel);

            var result = _vehicleRest.Update(vehicleModel);
            if (!result.Success)
                return View(vehicleModel);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var result = _vehicleRest.Find(new VehicleFilter { Id = id });

            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Index));

            if (!result.Success)
                return RedirectToAction(nameof(Index));
            return View(result.Model);
        }

        public IActionResult UpdateStatus()
        {
            return View(new VehicleModel());
        }
    }
}
