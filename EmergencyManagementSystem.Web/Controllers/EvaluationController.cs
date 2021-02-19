using EmergencyManagementSystem.Service.Enums;
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
    public class EvaluationController : Controller
    {

        private readonly IRequesterService _requesterService;
        private readonly IEmergencyRest _emergencyRest;
        private readonly UserService _userService;
        public EvaluationController(IRequesterService requesterService, IEmergencyRest emergencyRest, UserService userService)
        {
            _userService = userService;
            _requesterService = requesterService;
            _emergencyRest = emergencyRest;
        }

        public IActionResult Index()
        {
            LoadBag();
            return View(new EmergencyModel());
        }

        public ActionResult Update(long id)
        {
            var result = _emergencyRest.Find(new EmergencyFilter { Id = id });
            if (!result.Success)
            {
                LoadBag();
                ViewBag.Error = new List<string> { result?.Messages?.FirstOrDefault() ?? "Ocorreu um erro, favor tente novamente." };
                return View("index", new EmergencyModel());
            }
            var currentUser = _userService.GetCurrentUser();
            result.Model.EmployeeName = currentUser.EmployeeName;
            result.Model.EmployeeGuid = currentUser.EmployeeGuid;
            LoadBag();
            return View("index", result.Model);
        }

        public ActionResult Emergencies()
        {
            var emergenciesStatus = new[] { EmergencyStatus.InEvaluation, EmergencyStatus.InService };
            var emergencies = _emergencyRest.FindAll(new EmergencyFilter { EmergenciesStatus = emergenciesStatus });

            string HtmlTeste = "";
            foreach (var item in emergencies.Model)
            {
                string html =
                    $"<div class=\"info-box {item.GetClassByStatus()}\"><a href=\"{Url.Action("Update", "Evaluation", new { id = item.Id })}\"><div class=\"box-body\">" +
                    $"<h5><b>Oc: </b>{item.Id} <span class=\"pull-right\"><b>{item.Date.ToShortDateString()} {item.Date.ToShortTimeString()}</b></span></h5>" +
                    $"<h4>{item.Name}</h4></div></a></div>";
                HtmlTeste += html;
            }

            var teste = Json(HtmlTeste);
            return teste;
        }

        public void LoadBag()
        {
            var emergenciesStatus = new[] { EmergencyStatus.InEvaluation, EmergencyStatus.InService };
            var emergencies = _emergencyRest.FindAll(new EmergencyFilter { EmergenciesStatus = emergenciesStatus });
            if (emergencies.Success)
                ViewBag.Emergencies = emergencies.Model;
        }
    }
}
