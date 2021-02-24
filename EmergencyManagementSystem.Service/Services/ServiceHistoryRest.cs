using EmergencyManagementSystem.Service.Interfaces;
using EmergencyManagementSystem.Service.Models;
using Microsoft.Extensions.Configuration;

namespace EmergencyManagementSystem.Service.Services
{
    public class ServiceHistoryRest : RestBase<ServiceHistoryModel>, IServiceHistoryRest
    {
        public ServiceHistoryRest(IConfiguration configuration) 
            : base(configuration, "SAMUApi", "ServiceHistory")
        {
        }

        public Result SendVehicle(ServiceHistoryModel serviceHistoryModel)
        {
            return Post<Result, ServiceHistoryModel>(serviceHistoryModel, $"{_controller}/SendVehicle");
        }

        public Result CancelServiceHistory(ServiceCancellationHistoryModel serviceCancellation)
        {
            return Post<Result, ServiceCancellationHistoryModel>(serviceCancellation, $"{_controller}/CancelServiceHistory");
        }
    }
}
