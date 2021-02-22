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
    public class ServiceHisotryRest : RestBase<ServiceHistoryModel>, IServiceHisotryRest
    {
        public ServiceHisotryRest(IConfiguration configuration) 
            : base(configuration, "SAMUApi", "ServiceHistory")
        {
        }

        public Result SendVehicle(ServiceHistoryModel serviceHistoryModel)
        {
            return Post<Result, ServiceHistoryModel>(serviceHistoryModel, $"{_controller}/SendVehicle");
        }
    }
}
