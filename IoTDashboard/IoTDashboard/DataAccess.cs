using Newtonsoft.Json;
using System.IO;

namespace IoTDashboard
{
    public class DataAccess : IDataAccess
    {
        private const string devicesJsonPath = @".\IoTdevices.json";
        private readonly IFileReader _fileReader;
        public DataAccess(IFileReader fileReader)
        {
            _fileReader = fileReader;
        }

        public void ChangeDeviceState(int id)
        {
            var devices = GetAllDevices();
            var device = devices.FirstOrDefault(x=> x.Id ==id);
            if(device == null)
            {
                throw new ArgumentException();
            }
            device.State = ChangeState(device.State);
            _fileReader.WriteJsonToFIle(devices, devicesJsonPath);
        }

        public Device DeleteDevice(int id)
        {
            var devices = GetAllDevices();
            var device = devices.FirstOrDefault(x => x.Id == id);
            if (device == null)
            {
                throw new ArgumentException();
            }
            devices.Remove(device);
            _fileReader.WriteJsonToFIle(devices, devicesJsonPath);

            return device;
        }

        public IList<Device> GetAllDevices()
        {
            List<Device> devices = _fileReader.DecerializeFromFile<List<Device>>(devicesJsonPath);

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
