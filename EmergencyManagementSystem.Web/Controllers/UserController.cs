using EmergencyManagementSystem.Service.Filters;
using EmergencyManagementSystem.Service.Interfaces;
using EmergencyManagementSystem.Service.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;


namespace EmergencyManagementSystem.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserRest _userRest;
        private readonly UserService _userService;

        public UserController(IUserRest userRest, UserService userService)
        {
            _userRest = userRest;
            _userService = userService;
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

        public IActionResult Update(long employeeId)
        {
            return View(new UserModel() { EmployeeId = employeeId });
        }


        [HttpPost]
        public IActionResult Update(UserModel userModel)
        {
            if (userModel.NewPassword != userModel.NewPasswordConfirmation)
            {
                ViewBag.Error = new List<string> { "A nova senha e a confirmação da nova senha devem ser idênticas." };
                return View(new UserModel());
            }

            var currentUser = _userService.GetCurrentUser();
            userModel.Login = currentUser.Login;
            var user = _userRest.Find(new UserFilter { Login = userModel.Login, Password = userModel.Password });
            if (!user.Success)
            {
                ViewBag.Error = new List<string> { "A senha atual está incorreta." };
                return View(new UserModel());
            }
            user.Model.Password = userModel.NewPassword;
            var result = _userRest.Update(user.Model);
            if (!result.Success)
            {
                ViewBag.Error = new List<string> { "Erro ao alterar a senha do usuário.." };
                return View(userModel);
            }

            ViewBag.Error = new List<string> { "Nova senha cadastrada com sucesso." };
            return View(new UserModel());
        }
    }
}
