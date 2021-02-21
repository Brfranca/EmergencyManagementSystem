using EmergencyManagementSystem.Service.Interfaces;
using EmergencyManagementSystem.Service.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace EmergencyManagementSystem.Service.Services
{
    public class MedicalEvaluationRest : RestBase<MedicalEvaluationModel>, IMedicalEvaluationRest
    {
        public MedicalEvaluationRest(IConfiguration configuration) : base(configuration, "SAMUApi", "MedicalEvaluation")
        {
        }

        public Result RegisterEvaluations(List<MedicalEvaluationModel> evaluations)
        {
            return Post<Result, List<MedicalEvaluationModel>>(evaluations, $"{_controller}/RegisterEvaluations");
        }
    }
}
