using EmergencyManagementSystem.Service.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergencyManagementSystem.Service.Models
{
    public class VehicleModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Favor informar a placa do veículo.")]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "A placa deve ter 7 caracteres.")]
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

        
    }
}
