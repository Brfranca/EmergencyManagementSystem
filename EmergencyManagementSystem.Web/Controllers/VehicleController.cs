using EmergencyManagementSystem.Service.Enums;
using EmergencyManagementSystem.Service.Filters;
using EmergencyManagementSystem.Service.Interfaces;
using EmergencyManagementSystem.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EmergencyManagementSystem.Web.Controllers
{
    [Authorize]
    public class VehicleController : Controller
    {
        private readonly IVehicleRest _vehicleRest;

        public VehicleController(IVehicleRest vehicleRest)
        {
            _vehicleRest = vehicleRest;
        }

        public IActionResult Index(VehicleFilter vehiclefilter)
        {
            ViewBag.VehicleName = vehiclefilter.VehicleName;
            ViewBag.Plate = vehiclefilter.VehiclePlate;
            var vehicles = _vehicleRest.FindPaginated(vehiclefilter);
            return View(vehicles);
        }


        public IActionResult Register()
        {
            return View(new VehicleModel());
        }

        [HttpPost]
        public IActionResult Register(VehicleModel vehicleModel)
        {
            if (!ModelState.IsValid)
                return View(vehicleModel);

            vehicleModel.VehicleStatus = VehicleStatus.Cleared;
            var result = _vehicleRest.Register(vehicleModel);
            if (!result.Success)
            {
                ViewBag.Error = result.Messages;
                return View(vehicleModel);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(long id)
        {

            var result = _vehicleRest.Find(new VehicleFilter { Id = id });

            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Index));

            if (!result.Success)
                return RedirectToAction(nameof(Index));
            return View(result.Model);
        }

        [HttpPost]
        public IActionResult Update(VehicleModel vehicleModel)
        {

            if (!ModelState.IsValid)
                return View(vehicleModel);

            var result = _vehicleRest.Update(vehicleModel);
            if (!result.Success)
            {
                ViewBag.Error = result.Messages;
                return View(vehicleModel);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(long id)
        {
            var result = _vehicleRest.Find(new VehicleFilter { Id = id });

            if (!result.Success)
            {
                ViewBag.Error = new List<string> { "Erro ao mostrar os detalhes do veículo." };
                var vehicles = _vehicleRest.FindPaginated(new VehicleFilter());
                return View("Index", vehicles);
            }
            return View(result.Model);
        }

        public IActionResult Status(VehicleFilter vehicleFilter)
        {
            ViewBag.Codename = vehicleFilter.Codename;
            ViewBag.OperationCity = vehicleFilter.OperationCity;
            var vehicles = _vehicleRest.FindPaginated(vehicleFilter);
            return View(vehicles);
        }

        public IActionResult AlterStatus(long id, VehicleStatus currentStatus, int status)
        {
            if (currentStatus == VehicleStatus.InService)
            {
                ViewBag.Error = new List<string> { "Não é possível alterar o status de um veículo que está realizando um atendimento." };
                var vehicles = _vehicleRest.FindPaginated(new VehicleFilter());
                return View("Status",vehicles);
            }

            var result = _vehicleRest.Find(new VehicleFilter { Id = id });
            if (!result.Success)
            {
                ViewBag.Error = result.Messages;
                var vehicles = _vehicleRest.FindPaginated(new VehicleFilter());
                return View("Status", vehicles);
            }

            result.Model.VehicleStatus = (VehicleStatus)status;
            var resultUpdate = _vehicleRest.Update(result.Model);
            if (!resultUpdate.Success)
            {
                var vehicles = _vehicleRest.FindPaginated(new VehicleFilter());
                return View("Status", vehicles);
            }
            return RedirectToAction(nameof(Status));
        }

        public IActionResult Disable(int id)
        {
            var result = _vehicleRest.Find(new VehicleFilter { Id = id });
            if (!result.Success)
            {
                ViewBag.Error = result.Messages;
                var vehicles = _vehicleRest.FindPaginated(new VehicleFilter());
                return View("Index", vehicles);
            }

            result.Model.Active = (result.Model.Active == Active.Active) ? Active.Disabled : Active.Active;
            var resultDisable = _vehicleRest.Update(result.Model);
            if (!resultDisable.Success)
            {
                ViewBag.Error = new List<string> { $"Erro ao alterar o status do veículo." };
                var employees = _vehicleRest.FindPaginated(new VehicleFilter());
                return View("Index", employees);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
