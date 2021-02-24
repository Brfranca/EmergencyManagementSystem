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
        private readonly IServiceHistoryRest _serviceHistoryRest;
        private readonly IVehiclePositionHistoryRest _vehiclePositionHistoryRest;

        public VehicleManagementController(IEmergencyRest emergencyRest, UserService userService, IEmergencyHistoryRest emergencyHistoryRest,
            IEmergencyRequiredVehicleRest requiredVehicleRest, IVehicleRest vehicleRest, IEmployeeRest employeeRest,
            IMedicalEvaluationRest medicalEvaluationRest, IServiceHistoryRest serviceHistoryRest, IVehiclePositionHistoryRest vehiclePositionHistoryRest)
        {
            _serviceHistoryRest = serviceHistoryRest;
            _vehicleRest = vehicleRest;
            _userService = userService;
            _emergencyRest = emergencyRest;
            _emergencyHistoryRest = emergencyHistoryRest;
            _requiredVehicleRest = requiredVehicleRest;
            _employeeRest = employeeRest;
            _medicalEvaluationRest = medicalEvaluationRest;
            _vehiclePositionHistoryRest = vehiclePositionHistoryRest;
        }

        public IActionResult Index()
        {
            LoadBag();
            return View(new EmergencyModel());
        }

        [HttpPost]
        public IActionResult Register(EmergencyModel emergencyModel)
        {
            if (emergencyModel.Cancel)
            {
                return RedirectToAction("Cancel", emergencyModel);
            }
            emergencyModel.EmployeeGuid = _userService.GetCurrentUser().EmployeeGuid;


            LoadBag();
            return View("Index", new EmergencyModel());
        }

        public IActionResult Update(long emergencyId, long emergencyRequiredVehicleId)
        {

            var result = _emergencyRest.Find(new EmergencyFilter { Id = emergencyId });
            if (!result.Success)
            {
                LoadBag();
                ViewBag.Error = new List<string> { result?.Messages?.FirstOrDefault() ?? "Ocorreu um erro, favor tente novamente." };
                return View("Index", new EmergencyModel());
            }

            var requeridVehicle = result.Model.EmergencyRequiredVehicleModels.FirstOrDefault(d => d.Id == emergencyRequiredVehicleId);
            ViewBag.VehiclesAvaiable = _vehicleRest.FindAll(new VehicleFilter
            {
                Active = Active.Active,
                VehicleStatus = VehicleStatus.Cleared,
                VehicleType = requeridVehicle.VehicleType
            }).Model;

            ViewBag.EmergencyRequiredVehicleId = emergencyRequiredVehicleId;
            result.Model.ServiceHistoryModels = null;

            LoadBag();
            return View("Index", result.Model);

        }

        public IActionResult EmergencyData(ServiceHistoryModel commited)
        {
            var result = _emergencyRest.Find(new EmergencyFilter { Id = commited.EmergencyId });
            if (!result.Success)
            {
                LoadBag();
                ViewBag.Error = new List<string> { result?.Messages?.FirstOrDefault() ?? "Ocorreu um erro, favor tente novamente." };
                return View("Index", new EmergencyModel());
            }
            ViewBag.CurrentServiceHistory = commited;
            LoadBag();
            return View("Index", result.Model);
        }

        public IActionResult RegisterPosition(long serviceHistoryId, VehiclePosition vehiclePosition, long emergencyId)
        {
            VehiclePositionHistoryModel vehiclePositionHistory = new VehiclePositionHistoryModel
            {
                Date = DateTime.Now,
                EmergencyId = emergencyId,
                ServiceHistoryId = serviceHistoryId,
                VehiclePosition = vehiclePosition
            };

            var result = _vehiclePositionHistoryRest.Register(vehiclePositionHistory);
            if (!result.Success)
            {
                var resultE = _emergencyRest.Find(new EmergencyFilter { Id = emergencyId });
                if (!resultE.Success)
                    resultE.Model = new EmergencyModel();
                LoadBag();
                ViewBag.CurrentServiceHistory = resultE.Model.ServiceHistoryModels.FirstOrDefault(d => d.Id == serviceHistoryId);
                ViewBag.Error = new List<string> { result?.Messages?.FirstOrDefault() ?? "Ocorreu um erro, favor tente novamente." };
                return View("Index", resultE.Model);
            }

            var resultEmergency = _emergencyRest.Find(new EmergencyFilter { Id = emergencyId });
            if (!resultEmergency.Success)
            {
                LoadBag();
                ViewBag.CurrentServiceHistory = resultEmergency.Model.ServiceHistoryModels.FirstOrDefault(d => d.Id == serviceHistoryId);
                ViewBag.Error = new List<string> { resultEmergency?.Messages?.FirstOrDefault() ?? "Ocorreu um erro, favor tente novamente." };
                return View("Index", resultEmergency.Model);
            }

            ViewBag.CurrentServiceHistory = resultEmergency.Model.ServiceHistoryModels.FirstOrDefault(d => d.Id == serviceHistoryId);
            LoadBag();
            return View("Index", resultEmergency.Model);
        }

        public IActionResult CancelService(ServiceHistoryModel currentServiceHistory)
        {
            currentServiceHistory.ServiceHistoryStatus = ServiceHistoryStatus.Canceled;

            var user = _userService.GetCurrentUser();

            EmergencyHistoryModel emergencyHistoryModel = new EmergencyHistoryModel()
            {
                Date = DateTime.Now,
                EmergencyId = currentServiceHistory.EmergencyId,
                EmployeeGuid = user.EmployeeGuid,
            };

            MedicalDecisionHistoryModel medicalDecision = new MedicalDecisionHistoryModel()
            {
                Date = DateTime.Now,
                EmployeeGuid = user.EmployeeGuid,
                EmployeeName = user.EmployeeName,
                EmergencyId = currentServiceHistory.EmergencyId,
            };

            ServiceCancellationHistoryModel serviceCancellationHistory = new ServiceCancellationHistoryModel
            {
                EmergencyHistoryModel = emergencyHistoryModel,
                MedicalDecisionHistoryModel = medicalDecision,
                ServiceHistoryModel = currentServiceHistory
            };

            var result = _serviceHistoryRest.CancelServiceHistory(serviceCancellationHistory);
            if (!result.Success)
            {
                ViewBag.Error = result.Messages;
                LoadBag();
                return View("Index", new EmergencyModel());
            }

            LoadBag();
            return View("Index", new EmergencyModel());
        }

        public IActionResult SendVehicle(long vehicleId, long emergencyRequiredVehicleId)
        {
            var result = _serviceHistoryRest.SendVehicle(new ServiceHistoryModel
            {
                VehicleId = vehicleId,
                Date = DateTime.Now,
                EmergencyRequiredVehicleId = emergencyRequiredVehicleId
            });
            if (!result.Success)
            {
                ViewBag.Error = result.Messages;
                LoadBag();
                return View("Index", new EmergencyModel());
            }

            LoadBag();
            return View("Index", new EmergencyModel());
        }

        public ActionResult Cancel(EmergencyModel emergencyModel)
        {
            var user = _userService.GetCurrentUser();

            var resultFind = _requiredVehicleRest.Find(new RequiredVehicleFilter { Id = emergencyModel.RequiredVehicleId });
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
                EmployeeGuid = user.EmployeeGuid,
                EmergencyStatus = emergencyModel.EmergencyStatus,
                Description = emergencyModel.Description + " - Cancelamento de veículo " + resultFind.Model.VehicleType.GetEnumDescription()
            };

            MedicalDecisionHistoryModel medicalDecision = new MedicalDecisionHistoryModel()
            {
                Date = DateTime.Now,
                EmployeeGuid = user.EmployeeGuid,
                EmployeeName = user.EmployeeName,
                CodeColor = resultFind.Model.CodeColor,
                EmergencyId = emergencyModel.Id,
                Description = emergencyModel.Description + " - Cancelamento de veículo " + resultFind.Model.VehicleType
            };


            resultFind.Model.EmergencyHistoryModel = emergencyHistoryModel;
            resultFind.Model.MedicalDecisionHistoryModel = medicalDecision;
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
                foreach (var requiredVehicleModel in item.EmergencyRequiredVehicleModels.Where(d => d.Status == VehicleRequiredStatus.Opened))
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
