using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergencyManagementSystem.Service.Models
{
    public class EmployeeVehicleModel
    {
        public long MemberId { get; set; }
        public EmployeeModel EmployeeModel { get; set; }
        public VehicleModel VehicleModel { get; set; }

        public EmployeeVehicleModel()
        {
            EmployeeModel = new EmployeeModel();
            VehicleModel = new VehicleModel();
        }
    }
}
