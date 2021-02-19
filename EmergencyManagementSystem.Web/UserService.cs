using EmergencyManagementSystem.Service.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

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
                return new UserModel { EmployeeName = name, EmployeeGuid = GetGuidCookie(identity.Name) };
            }
            return new UserModel();
        }

        private Guid GetGuidCookie(string cookie)
        {
            if (cookie.IsGuid())
                return new Guid(cookie);

            return Guid.NewGuid();
        }
    }

    public static class GuidValidation
    {
        public static bool IsGuid(this string value)
        {
            Guid x;
            return Guid.TryParse(value, out x);
        }
    }
}
