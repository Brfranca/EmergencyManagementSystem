using EmergencyManagementSystem.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergencyManagementSystem.Service.Filters
{
    public class MemberFilter : FilterBase
    {
        public long Id { get; set; }
        public Guid EmployeeGuid { get; set; }
        public long VehicleId { get; set; }
        public VehicleModel VehicleModel { get; set; }

    }
}
