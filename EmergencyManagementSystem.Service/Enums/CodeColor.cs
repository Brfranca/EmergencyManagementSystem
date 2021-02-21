using System.ComponentModel;

namespace EmergencyManagementSystem.Service.Enums
{
    public enum CodeColor : short
    {
        [Description("Selecione")]
        Invalido,
        [Description("Vermelho")]
        Red,
        [Description("Amarelo")]
        Yellow,
        [Description("Verde")]
        Green,
        [Description("Azul")]
        Blue,
        [Description("")]
        NoColor
    }
}
