using System.Collections.Generic;
using X.PagedList;

namespace EmergencyManagementSystem.Service.Models
{
    public class MemberRegisterModel
    {
        //public List<MemberModel> MemberModels { get; set; }
        public MemberModel MemberModel { get; set; }
        public PagedList<EmployeeModel> EmployeeModels { get; set; }
        public PagedList<VehicleModel> VehicleModels { get; set; }
        public List<EmployeeVehicleModel> EmployeeVehicleModels { get; set; }

        public MemberRegisterModel()
        {
            EmployeeVehicleModels = new List<EmployeeVehicleModel>();
        }
    }
}
