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
        public EmergencyHistoryModel emergencyHistoryModel { get; set; }
    }
}
