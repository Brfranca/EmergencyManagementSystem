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
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View(new UserModel());
        }

        public IActionResult Create()
        {
            return View(new EmployeeModel());
        }

        public IActionResult Register()
        {
            return View(new EmployeeModel());
        }

        [HttpPost]
        public IActionResult Insert(EmployeeModel employeeModel)
        {

            return View();
        }
    }
}
