using EmergencyManagementSystem.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmergencyManagementSystem.Entities.Entities
{
    public class MedicalEvaluation : Entity<long>
    {
        public DateTime Date { get; set; }
        public int DoctorId { get; set; }
        public int EmergencyId { get; set; }
        public string Evaluation { get; set; }
        public int PatientId { get; set; }
    }
}
