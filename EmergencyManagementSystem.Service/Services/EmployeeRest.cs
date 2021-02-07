using EmergencyManagementSystem.Service.Interfaces;
using EmergencyManagementSystem.Service.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

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
