using EmergencyManagementSystem.Entities.Enums;
using EmergencyManagementSystem.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmergencyManagementSystem.Entities.Entities
{
    public class EmergencyData : EntityRecord<long>
    {
        public VehicleType VehicleType { get; set; }
        public int EmergencyId { get; set; }
        public CodeColor CodeColor { get; set; }
    }
}
