using System.ComponentModel;

namespace EmergencyManagementSystem.Service.Enums
{
    public enum Company : short
    {
        [Description("Selecionar")]
        Invalid,
        [Description("Serviço de Atendimento Móvel de Urgência (SAMU)")]
        SAMU,
        [Description("Corpo de bombeiros")]
        FireFighter
    }
}
