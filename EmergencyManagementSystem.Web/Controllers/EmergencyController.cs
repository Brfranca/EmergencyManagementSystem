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
    public class EmergencyController : Controller
    {
        public EmergencyController()
        {
        }

        public IActionResult Index()
        {
            //ViewBag.Name = emergencyFilter.Id;
            //ViewBag.CPF = emergencyFilter.Date;
            //var emergencies = _emergencyRest.FindPaginated(emergencyFilter);
            return View(new EmergencyModel());
        }

        public IActionResult Register()
        {
            return View(new EmergencyModel());
        }

        [HttpPost]
        public IActionResult Register(EmergencyModel emergencyModel)
        {
            return View();
        }


    }
}
