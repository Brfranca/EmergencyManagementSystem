using EmergencyManagementSystem.Entities.Enums;
using EmergencyManagementSystem.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmergencyManagementSystem.Entities.Entities
{
    public class VehiclePositionHistory : EntityRecord<long>
    {
        public VehicleStatus VehicleStatus { get; set; }
        public int VehicleTeamId { get; set; }
        public int EmergencyId { get; set; }
    }
}
