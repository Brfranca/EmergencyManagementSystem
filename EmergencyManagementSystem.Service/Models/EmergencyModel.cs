using EmergencyManagementSystem.Service.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmergencyManagementSystem.Service.Models
{
    public class EmergencyModel : ICloneable
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string RequesterName { get; set; }
        public string RequesterPhone { get; set; }
        public virtual AddressModel AddressModel { get; set; }
        public long AddressId { get; set; }
        public EmergencyStatus EmergencyStatus { get; set; }
        public List<EmergencyRequiredVehicleModel> EmergencyRequiredVehicleModels { get; set; }
        public List<PatientModel> PatientModels { get; set; }
        public List<MedicalEvaluationModel> MedicalEvaluationModels { get; set; }
        public List<MedicalDecisionHistoryModel> MedicalDecisionHistoryModels { get; set; }
        public List<ServiceHistoryModel> ServiceHistoryModels { get; set; }
        public Guid EmployeeGuid { get; set; }
        public string EmployeeName { get; set; }
        public string Description { get; set; }
        public bool Cancel { get; set; }
        public EmergencyModel()
        {
            AddressModel = new AddressModel();
            EmergencyRequiredVehicleModels = new List<EmergencyRequiredVehicleModel>();
            PatientModels = new List<PatientModel>();
            MedicalEvaluationModels = new List<MedicalEvaluationModel>();
            ServiceHistoryModels = new List<ServiceHistoryModel>();
            MedicalDecisionHistoryModels = new List<MedicalDecisionHistoryModel>();
        }

        public string GetClassByStatus()
        {
            var code = EmergencyRequiredVehicleModels
                .OrderBy(d => d.CodeColor)
                .FirstOrDefault()?.CodeColor ?? CodeColor.Yellow;

            switch (EmergencyStatus)
            {
                case EmergencyStatus.InService:
                case EmergencyStatus.Committed:
                    {
                        if (code == CodeColor.Blue)
                            return "bg-blue";
                        else if (code == CodeColor.Green)
                            return "bg-green";
                        else if (code == CodeColor.Red)
                            return "bg-red";
                        else if (code == CodeColor.Yellow)
                            return "bg-yellow";
                        return "";
                    }
                default:
                    return "";
            }
        }

        public object Clone()
        {
           return this.Clone();
        }
    }
}
