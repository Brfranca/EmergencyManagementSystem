using System.ComponentModel;

namespace EmergencyManagementSystem.Service.Enums
{
    public enum Company : short
    {
        [Description("Inválido")]
        Invalid,
        SAMU,
        Bombeiro
    }
}
