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
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(VehicleModel vehicleModel)
        {
            if (!ModelState.IsValid)
            {
                return View(vehicleModel);
            }
            return RedirectToAction();
        }
    }
}
