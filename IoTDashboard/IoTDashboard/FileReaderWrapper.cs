using Newtonsoft.Json;
using System.IO;

namespace IoTDashboard
{
    public class FileReaderWrapper : IFileReader
    {
        public T DecerializeFromFile<T>(string filePath)
             where T : new()
        {
            var result = new T();
            using (StreamReader r = new StreamReader(@".\IoTdevices.json"))
            {
                string json = r.ReadToEnd();
                result = JsonConvert.DeserializeObject<T>(json);
            }
            return result;
        }
    }
}
