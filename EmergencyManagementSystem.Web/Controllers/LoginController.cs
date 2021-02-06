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
                var user = _userRest.Find(new UserFilter { Login = userLogin.Login, Password = userLogin.Password });
                if (!user.Success)
                    return View("Index");

                SingIn(user.Model);

                if (User.Identity.IsAuthenticated)
                    return RedirectToAction("index", "Ocurrence");

                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                return View("Index");
            }
        }

        [HttpGet("Logoff")]
        [Authorize]
        public IActionResult Logoff()
        {
            var authenticationManager = Request.HttpContext;
            authenticationManager.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
            return RedirectToAction("Index");
        }

        private async void SingIn(UserModel usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Login),
                new Claim(ClaimTypes.Role, "Usuario_Comum")
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
