using EmergencyManagementSystem.Service.Models;

namespace EmergencyManagementSystem.Service.Interfaces
{
    public interface IEmergencyRest : IRestBase<EmergencyModel>
    {
        Result SimpleRegister(EmergencyModel model);
        Result Finish(EmergencyModel model);
    }
}
