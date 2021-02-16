using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergencyManagementSystem.Service.Enums
{
    public enum Occupation : short
    {
        [Description("Selecione")]
        Invalid,
        [Description("Técnico/a Auxiliar de Regulação Médica")]
        TARM,
        [Description("Rádio operador(a)")]
        RO,
        [Description("Médico/a")]
        Physician,
        [Description("Motorista")]
        Driver,
        [Description("Enfermeiro/a")]
        Nurse,
        [Description("Tecnico/a de enfermagem")]
        NursingTechnician,
        [Description("Piloto/a")]
        Pilot
    }
}
