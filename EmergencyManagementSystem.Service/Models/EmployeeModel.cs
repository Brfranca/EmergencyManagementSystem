using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using EmergencyManagementSystem.Service.Enums;

namespace EmergencyManagementSystem.Service.Models
{
    public class EmployeeModel
    {
        public long Id { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }
        public Company Company { get; set; }
        public string ProfessionalRegistration { get; set; }
        public  AddressModel AddressModel { get; set; }
        public long AddressId { get; set; }
        public Occupation Occupation { get; set; }

        public EmployeeModel()
        {
            AddressModel = new AddressModel();
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
