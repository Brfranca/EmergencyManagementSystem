using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergencyManagementSystem.Service.Filters
{
    public class VehicleFilter : FilterBase
    {
        public string VehicleName { get; set; }
        public int Year { get; set; }
    }
}
