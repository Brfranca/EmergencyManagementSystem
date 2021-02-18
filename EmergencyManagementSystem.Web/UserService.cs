using EmergencyManagementSystem.Service.Filters;
using EmergencyManagementSystem.Service.Interfaces;
using EmergencyManagementSystem.Service.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmergencyManagementSystem.Web
{
    public class UserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public UserModel GetCurrentUser()
        {
            var identity = _httpContextAccessor?.HttpContext?.User?.Identities?.FirstOrDefault();
            if (identity?.IsAuthenticated ?? false)
            {
                var name = identity.Claims.LastOrDefault().Value;
                return new UserModel { EmployeeName = name, Login = identity.Name };
            }
            return new UserModel();
        }
    }
}
