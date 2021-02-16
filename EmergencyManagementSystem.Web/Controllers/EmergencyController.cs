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
        public EmergencyController(IRequesterService requesterService, IEmergencyRest emergencyRest)
        {
            _requesterService = requesterService;
            _emergencyRest = emergencyRest;
        }

        public IActionResult Index()
        {
            //ViewBag.Name = emergencyFilter.Id;
            //ViewBag.CPF = emergencyFilter.Date;
            //var emergencies = _emergencyRest.FindPaginated(emergencyFilter);
            return View(new EmergencyModel());
        }

        public IActionResult Register()
        {
            return View(new EmergencyModel());
        }

        [HttpPost]
        public IActionResult Register(EmergencyModel emergencyModel)
        {
            if (string.IsNullOrWhiteSpace(emergencyModel.RequesterPhone))
            {
                ModelState.AddModelError("RequesterPhone", "Favor preencher telefone");
                return View("index", new EmergencyModel());
            }
            if ((emergencyModel?.Id ?? 0) == 0)
            {//register
                var requesterResult = _requesterService.Find(new RequesterFilter { Telephone = emergencyModel.RequesterPhone });
                if (!requesterResult.Success)
                {
                    ModelState.AddModelError("RequesterPhone", "Ocorreu algum erro favor tentar novamente!");
                    return View(new EmergencyModel());
                }
                emergencyModel.AddressId = requesterResult?.Model?.AddressId ?? 0;
                emergencyModel.AddressModel = requesterResult?.Model?.AddressModel;
                emergencyModel.Date = DateTime.Now;
                emergencyModel.EmergencyStatus = Service.Enums.EmergencyStatus.Opened;
                emergencyModel.RequesterName = requesterResult?.Model?.Name ?? "";
                emergencyModel.Name = "";
                var emergencyResult = _emergencyRest.SimpleRegister(emergencyModel);
                if (!emergencyResult.Success)
                {
                    ModelState.AddModelError("RequesterPhone", "Ocorreu algum erro favor tentar novamente!");
                    return View(new EmergencyModel());
                }
                emergencyModel.Id = emergencyResult.Id;
                return View("Index", emergencyModel);
            }
            else
            {//update
                var requesterResult = _requesterService.Register(new RequesterModel
                {
                    AddressModel = emergencyModel.AddressModel,
                    Name = emergencyModel.RequesterName,
                    Telephone = emergencyModel.RequesterPhone
                });
                if (!requesterResult.Success)
                {

                }
                var emergencyResult = _emergencyRest.Update(emergencyModel);
                if (!emergencyResult.Success)
                {

                }
                return View("Index", emergencyModel); //retornar todas as ocorrênciar em aberto
            }
        }
    }
}
