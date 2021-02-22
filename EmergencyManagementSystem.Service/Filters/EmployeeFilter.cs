
using EmergencyManagementSystem.Service.Enums;
using System;
using System.Collections.Generic;

namespace EmergencyManagementSystem.Service.Filters
{
    public class EmployeeFilter : FilterBase
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public Occupation Occupation { get; set; }
        public Guid Guid { get; set; }
        public bool IsMember { get; set; }
        public List<Guid> EmployeeGuidWorking { get; set; }
    }
}
