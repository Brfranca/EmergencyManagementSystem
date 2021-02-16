using System.ComponentModel;

namespace EmergencyManagementSystem.Service.Enums
{
    public enum Company : short
    {
        [Description("Selecione")]
        Invalid,
        [Description("SAMU")]
        SAMU,
        [Description("Bombeiros")]
        FireFighter
    }
}
