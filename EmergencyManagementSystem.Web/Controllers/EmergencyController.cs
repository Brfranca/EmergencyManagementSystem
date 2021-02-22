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
    public class EmergencyController : Controller
    {
        private readonly IRequesterService _requesterService;
        private readonly IEmergencyRest _emergencyRest;
        private readonly IEmergencyHistoryRest _emergencyHistoryRest;
        private readonly UserService _userService;
        public EmergencyController(IRequesterService requesterService, IEmergencyRest emergencyRest, UserService userService, IEmergencyHistoryRest emergencyHistoryRest)
        {
            _userService = userService;
            _requesterService = requesterService;
            _emergencyRest = emergencyRest;
            _emergencyHistoryRest = emergencyHistoryRest;
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
            var requesterResult = _requesterService.Find(new RequesterFilter { Telephone = result.Model.RequesterPhone });
            if (requesterResult.Success && requesterResult?.Model?.AddressModel != null)
            {
                result.Model.AddressModel = requesterResult?.Model?.AddressModel;
                result.Model.AddressModel.Id = 0;
            }
            LoadBag();
            return View("index", result.Model);
        }

        [HttpPost]
        public IActionResult Register(EmergencyModel emergencyModel)
        {
            emergencyModel.EmployeeGuid = _userService.GetCurrentUser().EmployeeGuid;
            if (emergencyModel.Cancel)
            {
                return RedirectToAction("Cancel", emergencyModel);
            }

            if (string.IsNullOrWhiteSpace(emergencyModel.RequesterPhone))
            {
                ModelState.AddModelError("RequesterPhone", "Favor preencher telefone");
                LoadBag();
                return View("index", new EmergencyModel());
            }
            if ((emergencyModel?.Id ?? 0) == 0)
            {//register
                var requesterResult = _requesterService.Find(new RequesterFilter { Telephone = emergencyModel.RequesterPhone });
                if (!requesterResult.Success)
                {
                    ViewBag.Error = new List<string>() { requesterResult?.Messages?.FirstOrDefault() ?? "Ocorreu um erro, favor tente novamente." };
                    LoadBag();
                    return View("Index", new EmergencyModel());
                }

                emergencyModel.AddressModel = requesterResult?.Model?.AddressModel;
                if (emergencyModel.AddressModel != null)
                    emergencyModel.AddressModel.Id = 0;

                emergencyModel.EmergencyStatus = EmergencyStatus.Opened;
                emergencyModel.RequesterName = requesterResult?.Model?.Name ?? "";
                emergencyModel.Name = "";
                emergencyModel.Date = DateTime.Now;

                var emergencyResult = _emergencyRest.SimpleRegister(emergencyModel);
                if (!emergencyResult.Success)
                {
                    ViewBag.Error = new List<string> { emergencyResult?.Messages?.FirstOrDefault() ?? "Ocorreu um erro, favor tente novamente." };
                    LoadBag();
                    return View("Index", emergencyModel);
                }
                emergencyModel.Id = emergencyResult.Id;

                LoadBag();
                return View("Index", emergencyModel);
            }
            else
            {//update
                var requesterResultFind = _requesterService.Find(new RequesterFilter { Telephone = emergencyModel.RequesterPhone });
                if (!requesterResultFind.Success)
                {
                    ViewBag.Error = new List<string> { requesterResultFind?.Messages?.FirstOrDefault() ?? "Ocorreu um erro, favor tente novamente." };
                    LoadBag();
                    return View("Index", emergencyModel);
                }
                if ((requesterResultFind?.Model?.Id ?? 0) == 0)
                {
                    var requesterResult = _requesterService.Register(new RequesterModel
                    {
                        AddressModel = emergencyModel.AddressModel,
                        Name = emergencyModel.RequesterName,
                        Telephone = emergencyModel.RequesterPhone
                    });
                    if (!requesterResult.Success)
                    {
                        ViewBag.Error = requesterResult.Messages;
                        LoadBag();
                        return View("Index", emergencyModel);
                    }
                }
                else
                {
                    requesterResultFind.Model.AddressModel = emergencyModel.AddressModel;
                    requesterResultFind.Model.Name = emergencyModel.RequesterName;
                    requesterResultFind.Model.Telephone = emergencyModel.RequesterPhone;
                    var requesterResult = _requesterService.Update(requesterResultFind.Model);
                    if (!requesterResult.Success)
                    {
                        ViewBag.Error = requesterResult.Messages;
                        LoadBag();
                        return View("Index", emergencyModel);
                    }
                }
                if (emergencyModel.EmergencyStatus == EmergencyStatus.Opened)
                    emergencyModel.EmergencyStatus = EmergencyStatus.InEvaluation;

                var emergencyResult = _emergencyRest.Update(emergencyModel);
                if (!emergencyResult.Success)
                {
                    ViewBag.Error = emergencyResult.Messages;
                    LoadBag();
                    return View("Index", emergencyModel);
                }

                LoadBag();
                return View("Index", new EmergencyModel());
            }
        }

        public ActionResult Cancel(EmergencyModel emergencyModel)
        {
            EmergencyHistoryModel emergencyHistoryModel = new EmergencyHistoryModel()
            {
                Date = DateTime.Now,
                EmergencyId = emergencyModel.Id,
                EmployeeGuid = emergencyModel.EmployeeGuid,
                EmergencyStatus = EmergencyStatus.Canceled,
                Description = emergencyModel.Description
            };
            var resultEmergency = _emergencyRest.Find(new EmergencyFilter { Id = emergencyModel.Id });
            if (!resultEmergency.Success)
            {
                ViewBag.Error = resultEmergency.Messages;
                LoadBag();
                return View("Index", emergencyModel);
            }
            resultEmergency.Model.EmergencyStatus = EmergencyStatus.Canceled;
            emergencyHistoryModel.EmergencyModel = resultEmergency.Model;
            var result = _emergencyHistoryRest.Register(emergencyHistoryModel);
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
            var emergenciesStatus = new[] { EmergencyStatus.InEvaluation, EmergencyStatus.Opened };
            var emergencies = _emergencyRest.FindAll(new EmergencyFilter { EmergenciesStatus = emergenciesStatus });

            string HtmlTeste = "";
            foreach (var item in emergencies.Model)
            {
                string html =
                    $"<div class=\"info-box {item.GetClassByStatus()}\"><a href=\"{Url.Action("Update", "Emergency", new { id = item.Id })}\"><div class=\"box-body\">" +
                    $"<h5><b>Oc: </b>{item.Id} <span class=\"pull-right\"><b>{item.Date.ToShortDateString()} {item.Date.ToShortTimeString()}</b></span></h5>" +
                    $"<h4>{item.Name}</h4></div></a></div>";
                HtmlTeste += html;
            }

            var teste = Json(HtmlTeste);
            return teste;
        }

        public void LoadBag()
        {
            var emergenciesStatus = new[] { EmergencyStatus.InEvaluation, EmergencyStatus.Opened };
            var emergencies = _emergencyRest.FindAll(new EmergencyFilter { EmergenciesStatus = emergenciesStatus });
            if (emergencies.Success)
                ViewBag.Emergencies = emergencies.Model;
        }
    }
}
