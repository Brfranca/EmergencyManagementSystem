using EmergencyManagementSystem.Service.Filters;
using EmergencyManagementSystem.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmergencyManagementSystem.Web.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberRest _memberRest;

        public MemberController(IMemberRest memberRest)
        {
            _memberRest = memberRest;
        }

        public IActionResult Index(MemberFilter filter)
        {
            ViewBag.VehicleId = filter.VehicleId;
            ViewBag.EmployeeGuid = filter.EmployeeGuid;
            var members = _memberRest.FindPaginated(filter);
            return View(members);
        }
    }
}
