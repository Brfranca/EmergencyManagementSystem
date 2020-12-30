using EmergencyManagementSystem.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmergencyManagementSystem.Entities.Entities
{
    public class Requester : Entity<int>
    {
        public string Name { get; set; }
        public string Telephone { get; set; }
        public int AddressId { get; set; }
    }
}
