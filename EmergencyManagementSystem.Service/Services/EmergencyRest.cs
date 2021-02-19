using EmergencyManagementSystem.Service.Filters;
using EmergencyManagementSystem.Service.Interfaces;
using EmergencyManagementSystem.Service.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergencyManagementSystem.Service.Services
{
    public class EmergencyRest : RestBase<EmergencyModel>, IEmergencyRest
    {
        public EmergencyRest(IConfiguration configuration) 
            : base(configuration, "SAMUApi", "Emergency")
        {
        }

        public Result SimpleRegister(EmergencyModel model)
        {
            return Post<Result, EmergencyModel>(model, $"{_controller}/SimpleRegister");
        }
    }
}
