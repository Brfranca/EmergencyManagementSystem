using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergencyManagementSystem.Service.Filters
{
    public class EmergencyFilter : FilterBase
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
    }
}
