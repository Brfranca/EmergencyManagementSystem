using EmergencyManagementSystem.Common.Extentions;
using EmergencyManagementSystem.Common.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmergencyManagementSystem.BLL
{
    public class Validator
    {
        private StringBuilder _errors;
        public Validator()
        {
            _errors = new StringBuilder();
        }

        public void AddError(string error)
        {
            if (error.HasValue())
                _errors.AppendLine(error);
        }

        public Response Validate()
        {
            if (_errors.Length == 0)
            {
                return Response.CreateSuccess();
            }
            return Response.CreateFailure(_errors.ToString());
        }
    }
}
