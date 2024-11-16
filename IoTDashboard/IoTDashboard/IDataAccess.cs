
namespace IoTDashboard
{
    public interface IDataAccess
    {
        IList<Device> GetAllDevices();
        void ChangeDeviceState(int id);

        Device DeleteDevice(int id);
    }
}