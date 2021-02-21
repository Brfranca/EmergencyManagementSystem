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
    public class VehicleManagementController : Controller
    {
        private readonly IEmergencyRest _emergencyRest;
        private readonly UserService _userService;
        private readonly IEmergencyHistoryRest _emergencyHistoryRest;
        private readonly IEmergencyRequiredVehicleRest _requiredVehicleRest;

        public VehicleManagementController(IEmergencyRest emergencyRest, UserService userService, IEmergencyHistoryRest emergencyHistoryRest, IEmergencyRequiredVehicleRest emergencyRequiredVehicleRest)
        {
            _userService = userService;
            _emergencyRest = emergencyRest;
            _emergencyHistoryRest = emergencyHistoryRest;
            _requiredVehicleRest = emergencyRequiredVehicleRest;
        }

        public IActionResult Index()
        {
            LoadBag();
            return View(new EmergencyModel());
        }

        [HttpPost]
        public IActionResult Register(EmergencyModel emergencyModel)
        {
            emergencyModel.EmployeeGuid = _userService.GetCurrentUser().EmployeeGuid;
            if (!string.IsNullOrWhiteSpace(emergencyModel.Description))
            {
                return RedirectToAction("Cancel", emergencyModel);
            }

            LoadBag();
            return View("Index", new EmergencyModel());
        }

        public ActionResult Cancel(EmergencyModel emergencyModel)
        {
            var resultFind = _requiredVehicleRest.Find(new RequiredVehicleFilter { Id = emergencyModel.EmergencyRequiredVehicleModels.FirstOrDefault().Id });
            if (!resultFind.Success)
            {
                ViewBag.Error = resultFind.Messages;
                LoadBag();
                return View("Index", emergencyModel);
            }

            EmergencyHistoryModel emergencyHistoryModel = new EmergencyHistoryModel()
            {
                Date = DateTime.Now,
                EmergencyId = emergencyModel.Id,
                EmployeeGuid = emergencyModel.EmployeeGuid,
                EmergencyStatus = emergencyModel.EmergencyStatus,
                Description = emergencyModel.Description + " - Cancelamento de veículo " + resultFind.Model.VehicleType.GetEnumDescription()
            };

            resultFind.Model.emergencyHistoryModel = emergencyHistoryModel;
            resultFind.Model.Status = VehicleRequiredStatus.Canceled;
            var result = _requiredVehicleRest.Update(resultFind.Model);
            if (!result.Success)
            {
                ViewBag.Error = result.Messages;
                LoadBag();
                return View("Index", emergencyModel);
            }

            LoadBag();
            return View("Index", new EmergencyModel());
        }

        public ActionResult Emergencies()
        {
            var emergenciesStatus = new[] { EmergencyStatus.Committed, EmergencyStatus.InService };
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
            var emergenciesStatus = new[] { EmergencyStatus.Committed, EmergencyStatus.InService };
            var emergencies = _emergencyRest.FindAll(new EmergencyFilter { EmergenciesStatus = emergenciesStatus });
            if (emergencies.Success)
                ViewBag.Emergencies = emergencies.Model;
        }
    }
}
