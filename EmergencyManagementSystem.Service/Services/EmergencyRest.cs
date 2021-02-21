using EmergencyManagementSystem.Service.Interfaces;
using EmergencyManagementSystem.Service.Models;
using Microsoft.Extensions.Configuration;

namespace EmergencyManagementSystem.Service.Services
{
    public class EmergencyRest : RestBase<EmergencyModel>, IEmergencyRest
    {
        public EmergencyRest(IConfiguration configuration) 
            : base(configuration, "SAMUApi", "Emergency")
        {
        }

        public Result Finish(EmergencyModel model)
        {
            return Post<Result, EmergencyModel>(model, $"{_controller}/Finish");
        }

        public Result SimpleRegister(EmergencyModel model)
        {
            return Post<Result, EmergencyModel>(model, $"{_controller}/SimpleRegister");
        }
    }
}
