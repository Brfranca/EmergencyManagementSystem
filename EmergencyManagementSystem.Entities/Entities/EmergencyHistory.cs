using EmergencyManagementSystem.Entities.Enums;
using EmergencyManagementSystem.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmergencyManagementSystem.Entities.Entities
{
    public class EmergencyHistory : EntityRecord<long>
    {
        public int EmergencyId { get; set; }
        public int EmployeeId { get; set; }
        public EmergencyStatus EmergencyStatus { get; set; }
        public string Description { get; set; }
    }
}
