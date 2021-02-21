using EmergencyManagementSystem.Service.Enums;
using System;
using System.Collections.Generic;

namespace EmergencyManagementSystem.Service.Models
{
    public class ServiceHistoryModel
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public virtual VehicleModel VehicleModel { get; set; }
        public long VehicleId { get; set; }
        public virtual EmergencyModel EmergencyModel { get; set; }
        public long EmergencyId { get; set; }
        public ServiceHistoryStatus ServiceHistoryStatus { get; set; }
        public string Description { get; set; }
        public List<TeamMemberModel> TeamMemberModels { get; set; }
        public List<VehiclePositionHistoryModel> VehiclePositionHistoryModels { get; set; }

    }
}
