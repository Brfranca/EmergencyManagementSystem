using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergencyManagementSystem.Service.Filters
{
    public class EmployeeFilter : FilterBase
    {
        public string Name { get; set; }
        public string CPF { get; set; }
    }
}
