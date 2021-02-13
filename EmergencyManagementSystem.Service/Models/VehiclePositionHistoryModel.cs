using EmergencyManagementSystem.Service.Enums;
using System;

namespace EmergencyManagementSystem.Service.Models
{
    public class VehiclePositionHistoryModel
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public VehiclePosition VehiclePosition { get; set; }
        public virtual ServiceHistory ServiceHistory { get; set; }
        public long ServiceHistoryId { get; set; }
        public virtual EmergencyModel Emergency { get; set; }
        public long EmergencyId { get; set; }
    }
}
