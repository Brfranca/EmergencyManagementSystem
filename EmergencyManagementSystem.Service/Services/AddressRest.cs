﻿using EmergencyManagementSystem.Service.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergencyManagementSystem.Service.Services
{
    public class AddressRest : RestBase<Result, AddressModel>
    {
        public AddressRest(IConfiguration configuration, string key)
            : base(configuration, "CommonApi", "Address")
        {
        }
    }
}
