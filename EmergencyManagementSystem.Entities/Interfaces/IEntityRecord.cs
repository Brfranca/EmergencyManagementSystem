using System;
using System.Collections.Generic;
using System.Text;

namespace EmergencyManagementSystem.Entities.Interfaces
{
    public interface IEntityRecord<T> : IEntity<T>
    {
        DateTime Date { get; set; }
    }
}
