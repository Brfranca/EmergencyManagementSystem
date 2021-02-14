
namespace EmergencyManagementSystem.Service.Filters
{
    public class VehicleFilter : FilterBase
    {
        public int Id { get; set; }
        public string VehicleName { get; set; }
        public string VehiclePlate { get; set; }
        public string Codename { get; set; }
        public string OperationCity { get; set; }
    }
}
