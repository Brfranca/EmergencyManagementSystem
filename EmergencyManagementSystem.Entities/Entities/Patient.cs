using EmergencyManagementSystem.Entities.Enums;
using EmergencyManagementSystem.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmergencyManagementSystem.Entities.Entities
{
    public class Patient : Entity<int>
    {
        public string Name { get; set; }
        public string Telephone { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public int EmergencyId { get; set; }
    }
}
