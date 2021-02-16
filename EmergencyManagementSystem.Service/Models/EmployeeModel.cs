using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using EmergencyManagementSystem.Service.Enums;
using Newtonsoft.Json;

namespace EmergencyManagementSystem.Service.Models
{
    public class EmployeeModel
    {
        public long Id { get; set; }
        public Guid Guid { get; set; }
        [Required(ErrorMessage = "Favor informar o nome completo.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter no mínimo 3 e no máximo 100 letras.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Favor informar o telefone.")]
        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }
        [Required(ErrorMessage = "Favor informar o e-mail.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Favor informar o CPF.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve ter 11 números.")]
        public string CPF { get; set; }
        [Required(ErrorMessage = "Favor informar o RG.")]
        public string RG { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }
        [Range(1, 2, ErrorMessage = "Favor informar a coorporação.")]
        public Company Company { get; set; }
        public string ProfessionalRegistration { get; set; }
        public AddressModel AddressModel { get; set; }
        public long AddressId { get; set; }
        [Range(1, 7, ErrorMessage = "Favor informar o cargo ocupacional.")]
        public Occupation Occupation { get; set; }
        public Active Active { get; set; }

        public EmployeeModel()
        {
            AddressModel = new AddressModel();
        }

        public string GetEnumDescription(Enum value)
        {
            return Utils.GetEnumDescription(value);
        }

        public string GetActive()
        {
            if (Active == Active.Active)
                return "Desativar";
            else
                return "Ativar";
        }

    }

    public static class Utils
    {
        public static string GetEnumDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }
    }
}
