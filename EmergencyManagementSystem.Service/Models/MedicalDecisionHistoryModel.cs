using EmergencyManagementSystem.Service.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergencyManagementSystem.Service.Models
{
    public class MedicalDecisionHistoryModel
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public long EmergencyId { get; set; }
        public Guid EmployeeGuid { get; set; }
        public string Description { get; set; }
        public CodeColor CodeColor { get; set; }
    }
}
