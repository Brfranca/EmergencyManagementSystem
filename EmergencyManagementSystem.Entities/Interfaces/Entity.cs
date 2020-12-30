using System;
using System.Collections.Generic;
using System.Text;

namespace EmergencyManagementSystem.Entities.Interfaces
{
    public class Entity<T> : IEntity<T>
    {
        public T Id { get; set; }
    }
}
