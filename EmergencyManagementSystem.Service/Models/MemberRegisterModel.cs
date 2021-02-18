using X.PagedList;

namespace EmergencyManagementSystem.Service.Models
{
    public class MemberRegisterModel
    {
        public PagedList<MemberModel> MemberModels { get; set; }
        public MemberModel MemberModel { get; set; }
        public PagedList<EmployeeModel> EmployeeModels { get; set; }
        public PagedList<VehicleModel> VehicleModels { get; set; }
        public PagedList<EmployeeModel> EmployeeMembers { get; set; }

    }
}
