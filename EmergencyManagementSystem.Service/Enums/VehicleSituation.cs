using System.ComponentModel;

namespace EmergencyManagementSystem.Service.Enums
{
    public enum VehicleSituation : short
    {
        [Description("Inválido")]
        Broken,
        Cleared,
        InService,
        NotCirculating
    }
}
