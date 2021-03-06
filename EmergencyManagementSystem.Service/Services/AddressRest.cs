﻿using EmergencyManagementSystem.Service.Interfaces;
using EmergencyManagementSystem.Service.Models;
using Microsoft.Extensions.Configuration;

namespace EmergencyManagementSystem.Service.Services
{
    public class AddressRest : RestBase<AddressModel>, IAddressRest
    {
        public AddressRest(IConfiguration configuration)
            : base(configuration, "CommonApi", "Address")
        {
        }
    }
}
