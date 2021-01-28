using System;
using System.Collections.Generic;
using System.Text;

namespace EmergencyManagementSystem.Entities.Entities
{
    public class TeamMember
    {
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public int VehicleTeamId { get; set; }
        public virtual VehicleTeam VehicleTeam { get; set; }
    }
}
