using EmergencyManagementSystem.Service.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergencyManagementSystem.Service.Services
{
    public class AddressRest : RestBase
    {
        public AddressRest(IConfiguration configuration, string key)
            : base(configuration, "CommonApi")
        {
        }

        //Ideias: Criar mais um CRUDRest com todos os método básico
        public Result Register(AddressModel addressModel)
        {
            return Post<Result, AddressModel>(addressModel, "Address/Register");
        }
    }
}
