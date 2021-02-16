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
        [Description("Selecione")]
        Invalid,
        [Description("Feminino")]
        Female,
        [Description("Masculino")]
        Male,
        [Description("Desconhecido")]
        Unknown
    }
}
