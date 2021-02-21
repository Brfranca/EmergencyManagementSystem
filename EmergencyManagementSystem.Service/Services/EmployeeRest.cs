using EmergencyManagementSystem.Service.Interfaces;
using EmergencyManagementSystem.Service.Models;
using Microsoft.Extensions.Configuration;

namespace EmergencyManagementSystem.Service.Services
{
    public class EmployeeRest : RestBase<EmployeeModel>, IEmployeeRest
    {
        public EmployeeRest(IConfiguration configuration) 
            : base(configuration, "CommonApi", "Employee")
        {
        }

       
    }
}
