using EmergencyManagementSystem.Service.Interfaces;
using EmergencyManagementSystem.Service.Models;
using Microsoft.Extensions.Configuration;

namespace EmergencyManagementSystem.Service.Services
{
    public class VehiclePositionHistoryRest : RestBase<VehiclePositionHistoryModel>, IVehiclePositionHistoryRest
    {
        public VehiclePositionHistoryRest(IConfiguration configuration) : base(configuration, "SAMUApi", "VehiclePositionHistory")
        {
        }
    }
}
