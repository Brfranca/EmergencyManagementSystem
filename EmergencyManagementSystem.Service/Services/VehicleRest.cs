﻿using EmergencyManagementSystem.Service.Interfaces;
using EmergencyManagementSystem.Service.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergencyManagementSystem.Service.Services
{
    public class VehicleRest : RestBase<VehicleModel>, IVehicleRest
    {
        public VehicleRest(IConfiguration configuration)
            : base(configuration, "SAMUApi", "Vehicle")
        {
        }
    }
}
