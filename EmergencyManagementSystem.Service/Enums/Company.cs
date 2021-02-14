using System.ComponentModel;

namespace EmergencyManagementSystem.Service.Enums
{
    public enum Company : short
    {
        [Description("Selecionar")]
        Invalid,
        [Description("SAMU")]
        SAMU,
        [Description("Bombeiros")]
        FireFighter
    }
}
