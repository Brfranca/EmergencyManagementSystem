using EmergencyManagementSystem.Common.Response;
using EmergencyManagementSystem.DAL.DAL;
using EmergencyManagementSystem.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmergencyManagementSystem.BLL.BLL
{
    public class AddressBLL
    {
        private readonly AddressDAL _addressDAL;
        public AddressBLL()
        {
            _addressDAL = new AddressDAL();
        }

        private Response Validate(Address address)
        {
            Validator validator = new Validator();
            //implementar validações

            return validator.Validate();
        }
    }
}
