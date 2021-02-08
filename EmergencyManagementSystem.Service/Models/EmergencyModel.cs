using EmergencyManagementSystem.Service.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergencyManagementSystem.Service.Models
{
    public class EmergencyModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string RequesterName { get; set; }
        public string RequesterPhone { get; set; }
        public virtual AddressModel AddressModel { get; set; }
        public long AddressId { get; set; }
        public EmergencyStatus EmergencyStatus { get; set; }
        public ICollection<EmergencyRequiredVehicleModel> EmergencyRequiredVehicles { get; set; }
        public ICollection<PatientModel> Patients { get; set; }

        public EmergencyModel()
        {
            AddressModel = new AddressModel();
            EmergencyRequiredVehicles = new List<EmergencyRequiredVehicleModel>();
            Patients = new List<PatientModel>();
        }
    }
}
