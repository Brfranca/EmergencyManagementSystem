using EmergencyManagementSystem.Service.Enums;
using EmergencyManagementSystem.Service.Filters;
using EmergencyManagementSystem.Service.Interfaces;
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
    public class EvaluationController : Controller
    {

        private readonly IRequesterService _requesterService;
        private readonly IEmergencyRest _emergencyRest;
        private readonly IMedicalEvaluationRest _medicalEvaluationRest;
        private readonly UserService _userService;
        public EvaluationController(IRequesterService requesterService, IEmergencyRest emergencyRest, UserService userService, IMedicalEvaluationRest medicalEvaluationRest)
        {
            _userService = userService;
            _requesterService = requesterService;
            _emergencyRest = emergencyRest;
            _medicalEvaluationRest = medicalEvaluationRest;
        }

        public IActionResult Index()
        {
            LoadBag();
            return View(new EmergencyModel());
        }

        [HttpPost]
        public ActionResult RegisterMedicalEvaluation(EmergencyModel emergencyModel, long patientId)
        {
            //como trazer os dados do paciente caso seja alterado na tela de avaliação?
            emergencyModel.EmployeeGuid = _userService.GetCurrentUser().EmployeeGuid;
            MedicalEvaluationModel medicalEvaluationModel = new MedicalEvaluationModel
            {
                EmergencyId = emergencyModel.Id,
                EmployeeGuid = emergencyModel.EmployeeGuid,
                Evaluation = emergencyModel.Description,
                Date = DateTime.Now,
                PatientId = patientId
            };

            var resultEmergency = _emergencyRest.Find(new EmergencyFilter { Id = emergencyModel.Id });
            if (!resultEmergency.Success)
            {
                ViewBag.Error = resultEmergency.Messages;
                LoadBag();
                return View("Index", emergencyModel);
            }

            var result = _medicalEvaluationRest.Register(medicalEvaluationModel);
            if (!result.Success)
            {
                ViewBag.Error = result.Messages;
                LoadBag();
                return View("Index", resultEmergency.Model);
            }

            LoadBag();
            return View("Index", resultEmergency.Model);
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
            result.Model.EmployeeName = _userService.GetCurrentUser().EmployeeName;
            result.Model.EmployeeGuid = _userService.GetCurrentUser().EmployeeGuid;
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
