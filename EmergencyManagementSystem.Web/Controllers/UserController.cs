using EmergencyManagementSystem.Service.Interfaces;
using EmergencyManagementSystem.Service.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmergencyManagementSystem.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRest _userRest;
        public UserController(IUserRest userRest)
        {
            _userRest = userRest;
        }

        public IActionResult Register()
        {
            return View(new UserModel());
        }

        [HttpPost]
        public IActionResult Register(UserModel userModel)
        {
            var result = _userRest.Register(userModel);
            if (!result.Success)
                return View(userModel);

            return RedirectToAction("Index", "Employee");
        }
    }
}
