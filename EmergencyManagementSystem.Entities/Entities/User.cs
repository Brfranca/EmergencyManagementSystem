using EmergencyManagementSystem.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmergencyManagementSystem.Entities.Entities
{
    public class User : Entity<int>
    {
        public int EmployeeId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
