using EmergencyManagementSystem.Service.Models;
using System.Collections.Generic;

namespace EmergencyManagementSystem.Service.Interfaces
{
    public interface IMedicalEvaluationRest : IRestBase<MedicalEvaluationModel>
    {
        Result RegisterEvaluations(List<MedicalEvaluationModel> evaluations);
    }
}
