using EmergencyManagementSystem.Service.Interfaces;
using EmergencyManagementSystem.Service.Models;
using Microsoft.Extensions.Configuration;

namespace EmergencyManagementSystem.Service.Services
{
    public class MedicalEvaluationRest : RestBase<MedicalEvaluationModel>, IMedicalEvaluationRest
    {
        public MedicalEvaluationRest(IConfiguration configuration) : base(configuration, "SAMUApi", "MedicalEvaluation")
        {
        }
    }
}
