using EmergencyManagementSystem.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergencyManagementSystem.Service.Filters
{
    public class UserFilter : IFilter
    {
        public string Login { get; set; }
    }
}
