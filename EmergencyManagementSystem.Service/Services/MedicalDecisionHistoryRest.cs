using EmergencyManagementSystem.Service.Interfaces;
using EmergencyManagementSystem.Service.Models;
using Microsoft.Extensions.Configuration;

namespace EmergencyManagementSystem.Service.Services
{
    public class MedicalDecisionHistoryRest : RestBase<MedicalDecisionHistoryModel>, IMedicalDecisionHistoryRest
    {
        public MedicalDecisionHistoryRest(IConfiguration configuration) 
            : base(configuration, "SAMUApi", "MedicalDecisionHistory")
        {
        }
    }
}
