using EmergencyManagementSystem.Service.Models;

namespace EmergencyManagementSystem.Service.Interfaces
{
    public interface IServiceHistoryRest : IRestBase<ServiceHistoryModel>
    {
        Result SendVehicle(ServiceHistoryModel serviceHistoryModel);
    }
}
