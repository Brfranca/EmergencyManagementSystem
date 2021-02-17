using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace EmergencyManagementSystem.Service.Models
{
    public class MemberRegisterViewModel
    {
        public PagedList<MemberModel> MemberModels { get; set; }
        public MemberModel MemberModel { get; set; }
        public PagedList<EmployeeModel> EmployeeModels { get; set; }
        public PagedList<VehicleModel> VehicleModels { get; set; }
        public PagedList<EmployeeModel> EmployeeMembers { get; set; }

    }
}
