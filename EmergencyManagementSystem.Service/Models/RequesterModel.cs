using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergencyManagementSystem.Service.Models
{
    public class RequesterModel
    {
        public long Id { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Telephone { get; set; }
        public int AddressId { get; set; }
        public AddressModel AddressModel { get; set; }
    }
}
