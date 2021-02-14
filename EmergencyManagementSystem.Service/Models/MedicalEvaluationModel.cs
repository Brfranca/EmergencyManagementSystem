using System;

namespace EmergencyManagementSystem.Service.Models
{
    public class MedicalEvaluationModel
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public Guid EmployeeGuid { get; set; }
        public virtual EmergencyModel Emergency { get; set; }
        public long EmergencyId { get; set; }
        public string Evaluation { get; set; }
        public virtual PatientModel Patient { get; set; }
        public long PatientId { get; set; }
    }
}
