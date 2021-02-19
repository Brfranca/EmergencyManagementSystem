using EmergencyManagementSystem.Service.Enums;
using System;

namespace EmergencyManagementSystem.Service.Models
{
    public class EmergencyHistoryModel
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public virtual EmergencyModel EmergencyModel { get; set; }
        public long EmergencyId { get; set; }
        public Guid EmployeeGuid { get; set; }
        public EmergencyStatus EmergencyStatus { get; set; }
        public string Description { get; set; }
    }
}
