using EmergencyManagementSystem.Service.Filters;
using EmergencyManagementSystem.Service.Interfaces;
using EmergencyManagementSystem.Service.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmergencyManagementSystem.Web.Controllers
{
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
            return View();
        }

        [HttpPost]
        public IActionResult Register(VehicleModel vehicleModel)
        {
            if (!ModelState.IsValid)
                return View(vehicleModel);

            return RedirectToAction();
        }
    }
}
