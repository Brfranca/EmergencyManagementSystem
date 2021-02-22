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
        private readonly IEmployeeRest _employeeRest;
        private readonly IMedicalDecisionHistoryRest _medicalDecisionHistoryRest;
        private readonly IEmergencyRequiredVehicleRest _emergencyRequiredVehicleRest;
        public EvaluationController(IRequesterService requesterService, IEmergencyRest emergencyRest, UserService userService,
            IMedicalEvaluationRest medicalEvaluationRest, IEmployeeRest employeeRest, IMedicalDecisionHistoryRest medicalDecisionHistoryRest,
            IEmergencyRequiredVehicleRest emergencyRequiredVehicleRest)
        {
            _medicalDecisionHistoryRest = medicalDecisionHistoryRest;
            _employeeRest = employeeRest;
            _userService = userService;
            _requesterService = requesterService;
            _emergencyRest = emergencyRest;
            _medicalEvaluationRest = medicalEvaluationRest;
            _emergencyRequiredVehicleRest = emergencyRequiredVehicleRest;
        }

        public IActionResult Index()
        {
            LoadBag();
            return View(new EmergencyModel());
        }

        public IActionResult Finish(long emergencyId)
        {
            var result = _emergencyRest.Finish(new EmergencyModel { Id = emergencyId });
            if (!result.Success)
            {
                ViewBag.Error = result.Messages;
                LoadBag();
                var resultEmergency = _emergencyRest.Find(new EmergencyFilter { Id = emergencyId });
                return View("Index", resultEmergency.Model);
            }

            return RedirectToAction("index");
        }

        public IActionResult RequestVehicle(long emergencyId, VehicleType vehicleType, CodeColor codeColor)
        {
            var user = _userService.GetCurrentUser();

            var result = _emergencyRequiredVehicleRest.Register(new EmergencyRequiredVehicleModel
            {
                CodeColor = codeColor,
                Date = DateTime.Now,
                EmergencyId = emergencyId,
                Status = VehicleRequiredStatus.Opened,
                VehicleType = vehicleType,
                MedicalDecisionHistoryModel = new MedicalDecisionHistoryModel
                {
                    Date = DateTime.Now,
                    EmergencyId = emergencyId,
                    EmployeeGuid =user.EmployeeGuid,
                    EmployeeName = user.EmployeeName,
                    CodeColor = codeColor,
                    Description = "Solicitado veículo"
                }
            });

            LoadBag();
            var resultEmergency = _emergencyRest.Find(new EmergencyFilter { Id = emergencyId });
            if (!result.Success)
            {
                ViewBag.Error = result.Messages;
                LoadBag();
                return View("Index", resultEmergency.Model);
            }
            return View("index", resultEmergency.Model);
        }

        public IActionResult MedicalDecision(long emergencyId, string orientation, CodeColor codeColor = CodeColor.NoColor)
        {
            var user = _userService.GetCurrentUser();
            var result = _medicalDecisionHistoryRest.Register(new MedicalDecisionHistoryModel
            {
                Description = orientation,
                Date = DateTime.Now,
                EmergencyId = emergencyId,
                EmployeeGuid = user.EmployeeGuid,
                EmployeeName = user.EmployeeName,
                CodeColor = codeColor
            });
            if (!result.Success)
            {
                ViewBag.Error = result.Messages;
                LoadBag();
                var resultE = _emergencyRest.Find(new EmergencyFilter { Id = emergencyId });
                return View("Index", resultE.Model);
            }

            var resultEmergency = _emergencyRest.Find(new EmergencyFilter { Id = emergencyId });
            LoadBag();
            return View("Index", resultEmergency.Model);
        }


        [HttpPost]
        public IActionResult RegisterMedicalEvaluation(EmergencyModel emergencyModel)
        {
            var resultEmergency = _emergencyRest.Find(new EmergencyFilter { Id = emergencyModel.Id });
            if (!resultEmergency.Success)
            {
                ViewBag.Error = resultEmergency.Messages;
                LoadBag();
                return View("Index", emergencyModel);
            }
            List<MedicalEvaluationModel> evaluations = new List<MedicalEvaluationModel>();
            var user = _userService.GetCurrentUser();

            foreach (var patient in emergencyModel.PatientModels)
            {
                MedicalEvaluationModel medicalEvaluationModel = new MedicalEvaluationModel
                {
                    EmergencyId = emergencyModel.Id,
                    EmployeeGuid = user.EmployeeGuid,
                    EmployeeName = user.EmployeeName,
                    Evaluation = patient.Description,
                    Date = DateTime.Now,
                    PatientId = patient.Id,
                    PatientModel = patient,
                };
                patient.EmergencyId = emergencyModel.Id;
                evaluations.Add(medicalEvaluationModel);
            }

            var result = _medicalEvaluationRest.RegisterEvaluations(evaluations);
            if (!result.Success)
            {
                ViewBag.Error = result.Messages;
                LoadBag();
                return View("Index", resultEmergency.Model);
            }
            resultEmergency = _emergencyRest.Find(new EmergencyFilter { Id = emergencyModel.Id });
            LoadBag();
            return View("Index", resultEmergency.Model);
        }

        public IActionResult Update(long id)
        {
            var result = _emergencyRest.Find(new EmergencyFilter { Id = id });
            if (!result.Success)
            {
                LoadBag();
                ViewBag.Error = new List<string> { result?.Messages?.FirstOrDefault() ?? "Ocorreu um erro, favor tente novamente." };
                return View("index", new EmergencyModel());
            }
            LoadBag();
            return View("index", result.Model);
        }

        public IActionResult Emergencies()
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
