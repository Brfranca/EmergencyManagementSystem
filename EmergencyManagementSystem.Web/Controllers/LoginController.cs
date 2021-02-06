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
    public class LoginController : Controller
    {
        private readonly IUserRest _userRest;
        public LoginController(IUserRest userRest)
        {
            _userRest = userRest;
        }

        public IActionResult Index()
        {
            return View(new UserModel());
        }

        [HttpPost]
        public IActionResult Login(UserModel userLogin)
        {
            try
            {
                var user = _userRest.Find(new UserFilter { Login = userLogin.Login });
                if (!user.Success)
                    return View("Index");
                if (user.Model.Password == userLogin.Password)
                    return RedirectToAction();

                return View("Index");
            }
            catch (Exception erro)
            {
                return View("Index");
            }
        }
    }
}
