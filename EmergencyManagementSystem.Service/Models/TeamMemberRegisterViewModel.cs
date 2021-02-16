using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace EmergencyManagementSystem.Service.Models
{
    public class TeamMemberRegisterViewModel
    {
        public TeamMemberModel TeamMemberModel { get; set; }
        public PagedList<EmployeeModel> employeeModels { get; set; }
    }
}
