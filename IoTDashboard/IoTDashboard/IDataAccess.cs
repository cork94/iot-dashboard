
namespace IoTDashboard
{
    public interface IDataAccess
    {
        IList<Device> GetAllDevices();
    }
}