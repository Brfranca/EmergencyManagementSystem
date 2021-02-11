using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergencyManagementSystem.Service.Enums
{
    public enum  EmergencyStatus : short
    {
        [Description("Inválido")]
        Invalid,
        [Description("Cancelada")]
        Canceled,
        [Description("Aberta")]
        Opened,
        [Description("Fechada")]
        Closed
    }
}
