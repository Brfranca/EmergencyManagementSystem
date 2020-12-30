using System;
using System.Collections.Generic;
using System.Text;

namespace EmergencyManagementSystem.Entities.Interfaces
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
