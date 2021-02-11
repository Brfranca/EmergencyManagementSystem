using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergencyManagementSystem.Service.Enums
{
    public enum  Gender : short
    {
        [Description("Inválido")]
        Female,
        Male,
        Unknown
    }
}
