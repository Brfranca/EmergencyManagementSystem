using EmergencyManagementSystem.Service.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergencyManagementSystem.Service.Services
{
    public class EmployeeRest : RestBase<EmployeeModel>
    {
        public EmployeeRest(IConfiguration configuration) 
            : base(configuration, "CommonApi", "Employee")
        {
        }
    }
}
