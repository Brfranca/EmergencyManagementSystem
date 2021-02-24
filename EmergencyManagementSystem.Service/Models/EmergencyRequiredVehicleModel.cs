using EmergencyManagementSystem.Service.Enums;
using System;

namespace EmergencyManagementSystem.Service.Models
{
    public class EmergencyRequiredVehicleModel
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public VehicleType VehicleType { get; set; }
        public EmergencyModel EmergencyModel { get; set; }
        public long EmergencyId { get; set; }
        public CodeColor CodeColor { get; set; }
        public VehicleRequiredStatus Status { get; set; }
        public EmergencyHistoryModel EmergencyHistoryModel { get; set; }
        public MedicalDecisionHistoryModel MedicalDecisionHistoryModel { get; set; }

        public string GetClassByColor()
        {
            switch (CodeColor)
            {
                case CodeColor.Red:
                    return "bg-red";
                case CodeColor.Yellow:
                    return "bg-yellow";
                case CodeColor.Green:
                    return "bg-green";
                case CodeColor.Blue:
                    return "bg-blue";
                default:
                    return "";
            }
        }
    }
}
