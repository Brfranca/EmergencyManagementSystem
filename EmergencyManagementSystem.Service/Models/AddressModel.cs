using System.ComponentModel.DataAnnotations;

namespace EmergencyManagementSystem.Service.Models
{
    public class AddressModel
    {
        public long Id { get; set; }
        public string CEP { get; set; }
        [Required(ErrorMessage = "Favor informar a cidade.")]
        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string Reference { get; set; }
    }
}
