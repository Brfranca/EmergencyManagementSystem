using EmergencyManagementSystem.Service.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergencyManagementSystem.Service.Models
{
    public class VehicleModel
    {
        public long Id { get; set; }
        public string VehiclePlate { get; set; }
        public string VehicleName { get; set; }
        public int Year { get; set; }
        public VehicleType VehicleType { get; set; }
        public VehicleSituation VehicleSituation { get; set; }
        //public ICollection<VehicleTeam> VehicleTeams { get; set; }
    }
}
