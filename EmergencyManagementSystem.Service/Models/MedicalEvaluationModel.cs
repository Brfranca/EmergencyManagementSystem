using System;

namespace EmergencyManagementSystem.Service.Models
{
    public class MedicalEvaluationModel
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public Guid EmployeeGuid { get; set; }
        public EmergencyModel EmergencyModel { get; set; }
        public long EmergencyId { get; set; }
        public string Evaluation { get; set; }
        public PatientModel PatientModel { get; set; }
        public long PatientId { get; set; }
        public string EmployeeName { get; set; }

        public MedicalEvaluationModel()
        {
            PatientModel = new PatientModel();
            EmergencyModel = new EmergencyModel();
        }
    }
}
