using EmergencyManagementSystem.Service.Interfaces;
using EmergencyManagementSystem.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmergencyManagementSystem.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRest _userRest;
        public UserController(IUserRest userRest)
        {
            _userRest = userRest;
        }

        public IActionResult Register(long employeeId)
        {
            return View(new UserModel() {EmployeeId = employeeId });
        }

        [HttpPost]
        public IActionResult Register(UserModel userModel)
        {
            if (!ModelState.IsValid)
                return View(userModel);

            var result = _userRest.Register(userModel);
            if (!result.Success)
            {
                ViewBag.Error = result.Messages;
                return View(userModel);
            }
            return RedirectToAction("Index", "Employee");
        }
    }
}
