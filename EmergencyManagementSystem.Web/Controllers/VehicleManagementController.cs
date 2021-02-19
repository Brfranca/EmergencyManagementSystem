using EmergencyManagementSystem.Service.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmergencyManagementSystem.Web.Controllers
{
    public class VehicleManagementController : Controller
    {
        public IActionResult Index()
        {
            return View(new EmergencyModel());
        }
    }
}
