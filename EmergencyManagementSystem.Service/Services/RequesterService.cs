using EmergencyManagementSystem.Service.Interfaces;
using EmergencyManagementSystem.Service.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergencyManagementSystem.Service.Services
{
    public class RequesterService : RestBase<RequesterModel>, IRequesterService
    {
        public RequesterService(IConfiguration configuration)
            : base(configuration, "CommonApi", "Requester")
        {
        }
    }
}
