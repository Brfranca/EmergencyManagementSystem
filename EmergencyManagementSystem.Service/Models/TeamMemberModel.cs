using EmergencyManagementSystem.Service.Enums;
using System;

namespace EmergencyManagementSystem.Service.Models
{
    public class TeamMemberModel
    {
        public long Id { get; set; }
        public DateTime StartedWork { get; set; }
        public DateTime FinishedWork { get; set; }
        public Guid EmployeeGuid { get; set; }
        public long ServieHistoryId { get; set; }
        public virtual ServiceHistory ServiceHistory { get; set; }
        public EmployeeStatus EmployeeStatus { get; set; }

    }
}
