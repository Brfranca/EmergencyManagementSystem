using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmergencyManagementSystem.Service.Enums
{
    public enum VehicleType : short
    {
        [Description("Selecionar")]
        Invalido,
        [Description("Suporte básico de vida terrestre (USB)")]
        USB,
        [Description("Suporte avançado de vida terrestre (USA)")]
        USA,
        [Description("Helicóptero")]
        HEL,
        [Description("Avião")]
        AVI
    }
}
