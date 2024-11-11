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
        public IList<Device> GetAllDevices()
        {
            List<Device> devices = _fileReader.DecerializeFromFile<List<Device>>(@".\IoTdevice.json");

            return devices;
        }
    }
}
