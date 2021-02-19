using EmergencyManagementSystem.Service.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmergencyManagementSystem.Service.Models
{
    public class EmergencyModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string RequesterName { get; set; }
        public string RequesterPhone { get; set; }
        public virtual AddressModel AddressModel { get; set; }
        public long AddressId { get; set; }
        public EmergencyStatus EmergencyStatus { get; set; }
        public List<EmergencyRequiredVehicleModel> EmergencyRequiredVehicles { get; set; }
        public List<PatientModel> Patients { get; set; }
        public List<MedicalEvaluationModel> MedicalEvaluations { get; set; }
        public Guid EmployeeGuid { get; set; }

        public EmergencyModel()
        {
            AddressModel = new AddressModel();
            EmergencyRequiredVehicles = new List<EmergencyRequiredVehicleModel>();
            Patients = new List<PatientModel>();
            MedicalEvaluations = new List<MedicalEvaluationModel>();
        }

        public string GetClassByStatus()
        {
            var code = EmergencyRequiredVehicles
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
    }
}
