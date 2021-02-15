using EmergencyManagementSystem.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmergencyManagementSystem.Web.Controllers
{
    public class TeamMemberController : Controller
    {
        private readonly ITeamMemberRest _teamMemberRest;

        public TeamMemberController(ITeamMemberRest teamMemberRest)
        {
            _teamMemberRest = teamMemberRest;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}
