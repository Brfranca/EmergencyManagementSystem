using EmergencyManagementSystem.Service.Filters;
using EmergencyManagementSystem.Service.Interfaces;
using EmergencyManagementSystem.Service.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmergencyManagementSystem.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserRest _userRest;
        private readonly UserService _userService;
        private readonly IEmployeeRest _employeeRest;
        public LoginController(IUserRest userRest, IEmployeeRest employeeRest,
            UserService userService)
        {
            _userRest = userRest;
            _userService = userService;
            _employeeRest = employeeRest;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("", "Emergency");

            return View(new UserModel());
        }

        [HttpPost]
        public IActionResult Login(UserModel userLogin)
        {
            try
            {
                var user = _userRest.Find(new UserFilter { Login = userLogin.Login, Password = userLogin.Password });
                if (!user.Success)
                {
                    ModelState.AddModelError("Login", "Login ou senha incorretos");
                    return View("Index", userLogin);
                }

                var employee = _employeeRest.Find(new EmployeeFilter { Id = user.Model.EmployeeId });
                if (!employee.Success)
                {
                    ModelState.AddModelError("Login", "Funcionário não encontrado");
                    return View("Index", userLogin);
                }
                SingIn(user.Model, employee.Model);

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet("Logout")]
        [Authorize]
        public IActionResult Logout()
        {
            var authenticationManager = Request.HttpContext;
            authenticationManager.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
            return RedirectToAction("Index");
        }

        private async void SingIn(UserModel usuario, EmployeeModel employee)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Login),
                new Claim(ClaimTypes.Role, employee.Name),
            };

            var identidadeDeUsuario = new ClaimsIdentity(claims, "Login");

            ClaimsPrincipal claimPrincipal = new ClaimsPrincipal(identidadeDeUsuario);

            var propriedadesDeAutenticacao = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTime.Now.ToLocalTime().AddHours(2),
                IsPersistent = true
            };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, propriedadesDeAutenticacao);
        }
    }
}
