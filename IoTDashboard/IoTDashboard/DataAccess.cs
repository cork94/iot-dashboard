using Newtonsoft.Json;
using System.IO;

namespace IoTDashboard
{
    public class DataAccess : IDataAccess
    {
        private readonly IFileReader _fileReader;
        public DataAccess(IFileReader fileReader)
        {
            _fileReader = fileReader;
        }

        public void ChangeDeviceState(int id)
        {
            var device = GetAllDevices().FirstOrDefault(x=> x.Id ==id);
            if(device == null)
            {
                throw new ArgumentException();
            }
            device.State = ChangeState(device.State);
        }

        public IList<Device> GetAllDevices()
        {
            List<Device> devices = _fileReader.DecerializeFromFile<List<Device>>(@".\IoTdevice.json");

            return devices;
        }

        private DeviceState ChangeState(DeviceState state)
        {
            switch (state)
            {
                case DeviceState.Running:
                    return DeviceState.Stopped;
                case DeviceState.Stopped:
                    return DeviceState.Running;
                default:
                    return state;
            }
        }
    }
}
