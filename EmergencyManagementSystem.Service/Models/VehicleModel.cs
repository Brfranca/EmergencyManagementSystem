using EmergencyManagementSystem.Service.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace EmergencyManagementSystem.Service.Models
{
    public class VehicleModel
    {
        public long Id { get; set; }
        public string VehiclePlate { get; set; }
        [Required(ErrorMessage = "Favor informar o codinome do veículo.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "O codinome deve ter no mínimo 3 e no máximo 20 caracteres.")]
        public string Codename { get; set; }
        [Required(ErrorMessage = "Favor informar o nome da cidade em que o veículo vai atuar.")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "O nome da cidade deve ter no mínimo 3 e no máximo 40 caracteres.")]
        public string OperationCity { get; set; }
        [Required(ErrorMessage = "Favor informar o nome/modelo do veículo.")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "O nome/modelo deve ter no mínimo 3 e no máximo 40 caracteres.")]
        public string VehicleName { get; set; }
        [Required(ErrorMessage = "Favor informar o ano do veículo.")]
        [Range(1000, 9999, ErrorMessage = "O ano deve conter 4 números.")]
        public int? Year { get; set; }
        [Range(1, 4, ErrorMessage = "Favor informar o tipo do veículo.")]
        public VehicleType VehicleType { get; set; }
        public VehicleStatus VehicleStatus { get; set; }
        public Active Active { get; set; }

        public string GetEnumDescription(Enum value)
        {
            return Utils.GetEnumDescription(value);
        }

        public string GetSelected(long vehicleId)
        {
            if (Id == vehicleId)
                return "Selected";
            return "";
        }

        public string GetActive()
        {
            if (Active == Active.Active)
                return "Desativar";
            else
                return "Ativar";
        }

        public string GetIconByType()
        {
            return VehicleType switch
            {
                VehicleType.USB => "fa-ambulance",
                VehicleType.USA => "fa-ambulance",
                VehicleType.HEL => "fa-helicopter",
                VehicleType.AVI => "fa-plane",
                _ => "",
            };
        }
    }
}
