using EmergencyManagementSystem.Entities.Enums;
using EmergencyManagementSystem.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmergencyManagementSystem.Entities.Entities
{
    public class VehicleTeam : EntityRecord<int>
    {
        public int VehicleId { get; set; }
        public int EmergencyId { get; set; }
        public VehicleTeamStatus vehicleTeamStatus { get; set; }
        public string Description { get; set; }
        public ICollection<TeamMember> TeamMembers { get; set; }
        public ICollection<VehiclePositionHistory> VehiclePositionHistories { get; set; }

        public VehicleTeam()
        {
            this.TeamMembers = new List<TeamMember>();
            this.VehiclePositionHistories = new List<VehiclePositionHistory>();
        }
    }
}
