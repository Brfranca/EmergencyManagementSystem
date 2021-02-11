using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmergencyManagementSystem.Service.Enums
{
    public enum VehicleType : short
    {
        [Description("Selecione")]
        [Display(Name = "Selecionar")]
        Invalido,
        [Display(Name = "USB")]
        USB,
        [Display(Name = "USA")]
        USA,
        [Display(Name = "HEL")]
        HEL,
        [Display(Name = "AVI")]
        AVI
    }
}
