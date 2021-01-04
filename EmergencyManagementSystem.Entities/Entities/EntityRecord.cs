using EmergencyManagementSystem.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmergencyManagementSystem.Entities.Entities
{
    public class EntityRecord<T> : IEntityRecord<T>
    {
        public T Id { get; set; }
        public DateTime Date { get; set; }
    }
}
