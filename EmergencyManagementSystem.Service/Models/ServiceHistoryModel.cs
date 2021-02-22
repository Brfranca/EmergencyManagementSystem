using EmergencyManagementSystem.Service.Enums;
using System;
using System.Collections.Generic;

namespace EmergencyManagementSystem.Service.Models
{
    public class ServiceHistoryModel
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public VehicleModel VehicleModel { get; set; }
        public long VehicleId { get; set; }
        public EmergencyModel EmergencyModel { get; set; }
        public long EmergencyId { get; set; }
        public ServiceHistoryStatus ServiceHistoryStatus { get; set; }
        public string Description { get; set; }
        public List<TeamMemberModel> TeamMemberModels { get; set; }
        public List<VehiclePositionHistoryModel> VehiclePositionHistoryModels { get; set; }
        public long EmergencyRequiredVehicleId { get; set; }

        public ServiceHistoryModel()
        {
            TeamMemberModels = new List<TeamMemberModel>();
            VehiclePositionHistoryModels = new List<VehiclePositionHistoryModel>();
            VehicleModel = new VehicleModel();
            EmergencyModel = new EmergencyModel();
        }

    }
}
