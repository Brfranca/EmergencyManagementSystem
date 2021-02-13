using System.ComponentModel;

namespace EmergencyManagementSystem.Service.Enums
{
    public enum VehicleStatus : short
    {
        [Description("Selecionar")]
        Invalid,
        [Description("Quebrado")]
        Broken,
        [Description("Liberado")]
        Cleared,
        [Description("Em serviço")]
        InService,
        [Description("Sem motorista")]
        NotCirculating
    }
}
