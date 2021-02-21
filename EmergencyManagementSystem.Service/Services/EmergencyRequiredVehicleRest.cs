using EmergencyManagementSystem.Service.Interfaces;
using EmergencyManagementSystem.Service.Models;
using Microsoft.Extensions.Configuration;

namespace EmergencyManagementSystem.Service.Services
{
    public class EmergencyRequiredVehicleRest : RestBase<EmergencyRequiredVehicleModel>, IEmergencyRequiredVehicleRest
    {
        public EmergencyRequiredVehicleRest(IConfiguration configuration) : base(configuration, "SAMUApi", "EmergencyRequiredVehicle")
        {
        }
    }
}
