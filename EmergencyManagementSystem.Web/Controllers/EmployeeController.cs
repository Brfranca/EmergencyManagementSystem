using EmergencyManagementSystem.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace EmergencyManagementSystem.Web.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        public IActionResult Index(int? pagina)
        {
            return View(new PagedList<EmployeeModel>(new List<EmployeeModel>()
            {
                new EmployeeModel(){ Name = "João Pereira", Company = Service.Enums.Company.Bombeiro, Email = "teste@hotmail.com", Telephone = "047 98483-0063"},
                new EmployeeModel{ Name = "Augusto Santos", Company = Service.Enums.Company.SAMU, Email = "teste@hotmail.com", Telephone = "047 98483-0063"}
            }, pagina ?? 1, 1));
        }

        public IActionResult Register()
        {
            return View(new EmployeeModel());
        }

        [HttpPost]
        public IActionResult Register(EmployeeModel employeeModel)
        {
            return View();
        }
    }
}
