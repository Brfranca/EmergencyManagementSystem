
using System.ComponentModel;

namespace EmergencyManagementSystem.Service.Enums
{
    public enum Active : short
    {
        [Description("Ativo")]
        Active,
        [Description("Inativo")]
        Disabled
    }
}
