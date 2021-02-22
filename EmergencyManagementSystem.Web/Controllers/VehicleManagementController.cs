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
    public class VehicleManagementController : Controller
    {
        private readonly IEmergencyRest _emergencyRest;
        private readonly UserService _userService;
        private readonly IEmergencyHistoryRest _emergencyHistoryRest;
        private readonly IEmergencyRequiredVehicleRest _requiredVehicleRest;
        private readonly IEmployeeRest _employeeRest;
        private readonly IMedicalEvaluationRest _medicalEvaluationRest;
        private readonly IVehicleRest _vehicleRest;
        private readonly IServiceHisotryRest serviceHisotryRest;
        public VehicleManagementController(IEmergencyRest emergencyRest, UserService userService, IEmergencyHistoryRest emergencyHistoryRest,
            IEmergencyRequiredVehicleRest requiredVehicleRest, IVehicleRest vehicleRest,
            IEmployeeRest employeeRest, IMedicalEvaluationRest medicalEvaluationRest)
        {
            _vehicleRest = vehicleRest;
            _userService = userService;
            _emergencyRest = emergencyRest;
            _emergencyHistoryRest = emergencyHistoryRest;
            _requiredVehicleRest = requiredVehicleRest;
            _employeeRest = employeeRest;
            _medicalEvaluationRest = medicalEvaluationRest;
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

        public IActionResult Update(long emergencyId, long emergencyRequiredVehicleId)
        {

            //tratar a nivel de veículo
            //var result = GetEmergencyModel(emergencyId);
            var result = _emergencyRest.Find(new EmergencyFilter { Id = emergencyId });
            if (!result.Success)
            {
                LoadBag();
                ViewBag.Error = new List<string> { result?.Messages?.FirstOrDefault() ?? "Ocorreu um erro, favor tente novamente." };
                return View("index", new EmergencyModel());
            }

            var requeridVehicle = result.Model.EmergencyRequiredVehicleModels.FirstOrDefault(d => d.Id == emergencyRequiredVehicleId);
            ViewBag.VehiclesAvaiable = _vehicleRest.FindAll(new VehicleFilter
            {
                Active = Active.Active,
                VehicleStatus = VehicleStatus.Cleared,
                VehicleType = requeridVehicle.VehicleType
            }).Model;

            ViewBag.EmergencyRequiredVehicleId = emergencyRequiredVehicleId;

            LoadBag();
            return View("index", result.Model);

        }

        public IActionResult SendVehicle(long vehicleId, long emergencyRequiredVehicleId)
        {
            //var result = _requiredVehicleRest.SendVehicle(new ServiceHistoryModel
            //{
            //    VehicleId = vehicleId,
            //    Date = DateTime.Now,
            //    EmergencyRequiredVehicleId = emergencyRequiredVehicleId,
            //});

            return null;
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

            resultFind.Model.EmergencyHistoryModel = emergencyHistoryModel;
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

        private Result<EmergencyModel> GetEmergencyModel(long id)
        {
            var resultEmergency = _emergencyRest.Find(new EmergencyFilter { Id = id });
            if (!resultEmergency.Success)
                return resultEmergency;

            foreach (var medicalEvaluation in resultEmergency.Model.MedicalEvaluationModels)
            {
                var employee = _employeeRest.Find(new EmployeeFilter { Guid = medicalEvaluation.EmployeeGuid });
                medicalEvaluation.EmployeeName = employee.Model.Name;
            }
            return resultEmergency;
        }

        public ActionResult Emergencies()
        {
            var emergenciesStatus = new[] { EmergencyStatus.Committed, EmergencyStatus.InService };
            var emergencies = _emergencyRest.FindAll(new EmergencyFilter { EmergenciesStatus = emergenciesStatus });

            string HtmlTeste = "";
            foreach (var item in emergencies.Model)
            {
                foreach (var requiredVehicleModel in item.EmergencyRequiredVehicleModels)
                {
                    string html =
                    $"<div class=\"info-box {requiredVehicleModel.GetClassByColor()}\"><a href=\"{Url.Action("Update", "VehicleManagement", new { emergencyId = item.Id, emergencyRequiredVehicleId = requiredVehicleModel.Id })}\"><div class=\"box-body\">" +
                    $"<h5><b>Oc: </b>{item.Id} <span class=\"pull-right\"><b>{item.Date.ToShortDateString()} {item.Date.ToShortTimeString()}</b></span></h5>" +
                    $"<h4>{item.Name}</h4></div></a></div>";
                    HtmlTeste += html;
                }
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
