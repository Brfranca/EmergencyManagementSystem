using EmergencyManagementSystem.Service.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace EmergencyManagementSystem.Service.Models
{
    public class MemberRegisterModel
    {
        public MemberModel MemberModel { get; set; }
        public IPagedList<EmployeeModel> EmployeeModels { get; set; }
        public List<VehicleModel> VehicleModels { get; set; }
        public List<EmployeeVehicleModel> EmployeeVehicleModels { get; set; }

        public MemberRegisterModel()
        {
            EmployeeVehicleModels = new List<EmployeeVehicleModel>();
            VehicleModels = new List<VehicleModel>();
            EmployeeVehicleModels = new List<EmployeeVehicleModel>();
        }

        public Occupation[] GetOccupations()
        {
            var ocupations = (Occupation[])Enum.GetValues(typeof(Occupation));
            ocupations = ocupations.ToListAsync().Result.Where(d => d != Occupation.RO && d != Occupation.TARM).ToArray();
            return ocupations;
        }

        public string GetOcccupationSelected(Occupation currentOccupation, Occupation suit)
        {
            if (currentOccupation == suit)
                return "Selected";
            return "";
        }
    }
}
