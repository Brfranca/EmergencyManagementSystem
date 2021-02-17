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
        public EmergencyController(IRequesterService requesterService, IEmergencyRest emergencyRest)
        {
            _requesterService = requesterService;
            _emergencyRest = emergencyRest;
        }

        public IActionResult Index()
        {
            LoadBag();

            //ViewBag.Name = emergencyFilter.Id;
            //ViewBag.CPF = emergencyFilter.Date;
            //var emergencies = _emergencyRest.FindPaginated(emergencyFilter);
            return View(new EmergencyModel());
        }

        [HttpPost]
        public IActionResult Register(EmergencyModel emergencyModel)
        {
            LoadBag();

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
                    //adicionar mensagem de erro
                    return View(emergencyModel); //colocar um new e n retornar os dados
                }

                emergencyModel.AddressId = requesterResult?.Model?.AddressId ?? 0;
                emergencyModel.AddressModel = requesterResult?.Model?.AddressModel;
                emergencyModel.EmergencyStatus = Service.Enums.EmergencyStatus.Opened;
                emergencyModel.RequesterName = requesterResult?.Model?.Name ?? "";
                emergencyModel.Name = "";
                emergencyModel.Date = DateTime.Now;

                var emergencyResult = _emergencyRest.SimpleRegister(emergencyModel);
                if (!emergencyResult.Success)
                {
                    //Adicionar mensagem de erro
                    return View(emergencyModel);
                }
                emergencyModel.Id = emergencyResult.Id;

                return View("Index", emergencyModel);
            }
            else
            {//update
                var requesterResultFind = _requesterService.Find(new RequesterFilter { Telephone = emergencyModel.RequesterPhone });
                if (!requesterResultFind.Success)
                {
                    //adicionar mensagem de erro.
                    return View(emergencyModel);
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
                        //adicionar mensagem de erro.
                        return View(emergencyModel);
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
                        //adicionar mensagem de erro.
                        return View(emergencyModel);
                    }
                }
                if (emergencyModel.EmergencyStatus == Service.Enums.EmergencyStatus.Opened)
                    emergencyModel.EmergencyStatus = Service.Enums.EmergencyStatus.InEvaluation;

                var emergencyResult = _emergencyRest.Update(emergencyModel);
                if (!emergencyResult.Success)
                {
                    //adicionar mensagem de erro.
                    return View(emergencyModel);
                }
                //retornar todas as ocorrênciar em aberto
                return View("Index", new EmergencyModel());
            }
        }

        public void LoadBag()
        {
            //chamar o o método LoadBag em todos os retornos para a tela de index.
            //Até o momento ja está

            var filter = new[] { EmergencyStatus.InEvaluation, EmergencyStatus.InService };
            //Filtar todas as ocorrências com os status acima
            //ViewBag.Emergencies = _emergencyRest.GetEmergencies();
            //Criar foreach na view e preencher com as emergencias da viewBag
        }
    }
}
