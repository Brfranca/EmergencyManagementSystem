using EmergencyManagementSystem.Entities.Enums;
using EmergencyManagementSystem.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmergencyManagementSystem.Entities.Entities
{
    public class Emergency : EntityRecord<long>
    {
        public int RequesterId { get; set; }
        public Address Address { get; set; }
        public EmergencyStatus EmergencyStatus { get; set; }
        public ICollection<MedicalEvaluation> MedicalEvaluations { get; set; }
        public ICollection<EmergencyHistory> EmergencyHistories { get; set; }
        public ICollection<Patient> Patients { get; set; }
        public ICollection<VehicleTeam> VehicleTeams { get; set; }
        public ICollection<EmergencyData> EmergencyDatas { get; set; }

        public Emergency()
        {
            this.MedicalEvaluations = new List<MedicalEvaluation>();
            this.EmergencyHistories = new List<EmergencyHistory>();
            this.Patients = new List<Patient>();
            this.VehicleTeams = new List<VehicleTeam>();
            this.EmergencyDatas = new List<EmergencyData>();
        }


    }
}
