using EmergencyManagementSystem.Entities.Enums;
using EmergencyManagementSystem.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmergencyManagementSystem.Entities.Entities
{
    public class Employee : Entity<int>
    {
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public DateTime BirthDate { get; set; }
        public int AddressId { get; set; }
        public Occupation Occupation { get; set; }
        public string CRM { get; set; }
    }
}
