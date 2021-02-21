using System.ComponentModel;

namespace EmergencyManagementSystem.Service.Enums
{
    public enum EmergencyStatus : short
    {
        [Description("Selecione")]
        Invalid,
        [Description("Cancelada")]
        Canceled,
        [Description("Aberta")] //Foi aberta
        Opened,
        [Description("Em Avaliação")] // Passou para o médico
        InEvaluation,
        [Description("Em Atendimento")] //Passou para o RO
        InService,
        [Description("Empenhada")] //Foi empenhada
        Committed,
        [Description("Fechada")] //Foi Finalizada
        Closed
    }
}
