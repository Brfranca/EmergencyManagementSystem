﻿using EmergencyManagementSystem.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergencyManagementSystem.Service.Filters
{
    public class UserFilter : FilterBase
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
