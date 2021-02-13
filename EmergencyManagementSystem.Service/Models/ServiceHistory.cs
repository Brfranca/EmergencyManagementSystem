using EmergencyManagementSystem.Service.Enums;
using System;
using System.Collections.Generic;

namespace EmergencyManagementSystem.Service.Models
{
    public class ServiceHistory
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public virtual VehicleModel Vehicle { get; set; }
        public long VehicleId { get; set; }
        public virtual EmergencyModel Emergency { get; set; }
        public long EmergencyId { get; set; }
        public ServiceHistoryStatus ServiceHistoryStatus { get; set; }
        public string Description { get; set; }
        public ICollection<TeamMemberModel> TeamMembers { get; set; }
        public ICollection<VehiclePositionHistoryModel> VehiclePositionHistories { get; set; }

    }
}
