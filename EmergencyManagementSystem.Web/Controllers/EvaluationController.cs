﻿using EmergencyManagementSystem.Service.Enums;
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
        public EvaluationController(IRequesterService requesterService, IEmergencyRest emergencyRest, UserService userService,
            IMedicalEvaluationRest medicalEvaluationRest, IEmployeeRest employeeRest)
        {
            _employeeRest = employeeRest;
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
        public ActionResult RegisterMedicalEvaluation(EmergencyModel emergencyModel)
        {
            var resultEmergency = GetEmergencyModel(emergencyModel.Id);
            if (!resultEmergency.Success)
            {
                ViewBag.Error = resultEmergency.Messages;
                LoadBag();
                return View("Index", emergencyModel);
            }
            //como trazer os dados do paciente caso seja alterado na tela de avaliação?
            List<MedicalEvaluationModel> evaluations = new List<MedicalEvaluationModel>();
            foreach (var patient in emergencyModel.Patients)
            {
                MedicalEvaluationModel medicalEvaluationModel = new MedicalEvaluationModel
                {
                    EmergencyId = emergencyModel.Id,
                    EmployeeGuid = emergencyModel.EmployeeGuid,
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
            resultEmergency = GetEmergencyModel(emergencyModel.Id);
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
            //Não está trazendo o PatientModel para pegar o nome do paciente
            var resultEvaluation = _medicalEvaluationRest.FindAll(new MedicalEvaluationFilter { EmergencyId = id });
            if (!resultEvaluation.Success)
            {
                LoadBag();
                ViewBag.Error = new List<string> { result?.Messages?.FirstOrDefault() ?? "Ocorreu um erro, favor tente novamente." };
                return View("index", new EmergencyModel());
            }
            result.Model.MedicalEvaluations = resultEvaluation.Model;
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

        private Result<EmergencyModel> GetEmergencyModel(long id)
        {
            var resultEmergency = _emergencyRest.Find(new EmergencyFilter { Id = id });
            if (!resultEmergency.Success)
                return resultEmergency;

            foreach (var medicalEvaluation in resultEmergency.Model.MedicalEvaluations)
            {
                var employee = _employeeRest.Find(new EmployeeFilter { Guid = medicalEvaluation.EmployeeGuid });
                medicalEvaluation.EmployeeName = employee.Model.Name;
            }
            return resultEmergency;
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
