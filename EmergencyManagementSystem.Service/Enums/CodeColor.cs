using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergencyManagementSystem.Service.Enums
{
    public enum CodeColor : short
    {
        [Description("Selecionar")]
        Invalido,
        [Description("Vermelho")]
        Red,
        [Description("Amarelo")]
        Yellow,
        [Description("Verde")]
        Green,
        [Description("Azul")]
        Blue
    }
}
