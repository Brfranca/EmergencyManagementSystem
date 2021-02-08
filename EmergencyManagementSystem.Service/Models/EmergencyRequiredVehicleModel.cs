using EmergencyManagementSystem.Service.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergencyManagementSystem.Service.Models
{
    public class EmergencyRequiredVehicleModel
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public VehicleType VehicleType { get; set; }
        public virtual EmergencyModel EmergencyModel { get; set; }
        public long EmergencyId { get; set; }
        public CodeColor CodeColor { get; set; }
    }
}
