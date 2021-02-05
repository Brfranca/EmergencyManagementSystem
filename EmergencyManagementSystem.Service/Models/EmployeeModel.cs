using System;
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
        public DateTime BirthDate { get; set; }
        public Company Company { get; set; }
        public string ProfessionalRegistration { get; set; }
        public AddressModel AddressModel { get; set; }
        public long AddressId { get; set; }
        public OccupationModel OccupationModel { get; set; }
        public short OccupationId { get; set; }
    }
}
