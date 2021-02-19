using EmergencyManagementSystem.Service.Interfaces;
using EmergencyManagementSystem.Service.Models;
using Microsoft.Extensions.Configuration;

namespace EmergencyManagementSystem.Service.Services
{
    public class EmergencyHistoryRest : RestBase<EmergencyHistoryModel>, IEmergencyHistoryRest
    {
        public EmergencyHistoryRest(IConfiguration configuration) : base(configuration, "SAMUApi", "EmergencyHistory")
        {
        }
    }
}
