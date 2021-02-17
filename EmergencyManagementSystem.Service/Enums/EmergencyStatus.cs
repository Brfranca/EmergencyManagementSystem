using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
