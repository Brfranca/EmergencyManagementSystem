using EmergencyManagementSystem.Entities.Enums;
using EmergencyManagementSystem.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmergencyManagementSystem.Entities.Entities
{
    public class Vehicle : Entity<int>
    {
        public string Plaque { get; set; }
        public string VehicleName { get; set; }
        public int Year { get; set; }
        public VehicleType VehicleType { get; set; }
        public VehicleSituation VehicleSituation { get; set; }
    }
}
