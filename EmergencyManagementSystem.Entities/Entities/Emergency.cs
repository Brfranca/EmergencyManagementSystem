using EmergencyManagementSystem.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmergencyManagementSystem.Entities.Entities
{
    public class Emergency : Entity<long>
    {
        public DateTime Date { get; set; }

    }
}
