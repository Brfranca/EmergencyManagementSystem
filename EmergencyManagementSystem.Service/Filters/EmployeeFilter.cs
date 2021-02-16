
using EmergencyManagementSystem.Service.Enums;

namespace EmergencyManagementSystem.Service.Filters
{
    public class EmployeeFilter : FilterBase
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public Occupation Occupation { get; set; }
    }
}
