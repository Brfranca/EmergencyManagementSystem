using EmergencyManagementSystem.Service.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergencyManagementSystem.Service.Models
{
    public class PatientModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public virtual EmergencyModel EmergencyModel { get; set; }
        public long EmergencyId { get; set; }
    }
}
