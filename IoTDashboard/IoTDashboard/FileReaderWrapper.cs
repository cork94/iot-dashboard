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
            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                result = JsonConvert.DeserializeObject<T>(json);
            }
            return result;
        }

        public void WriteJsonToFIle<T>(T objectToWrite, string filePath)
        {
            using (var w = new StreamWriter(filePath, false)) 
            {
                var stringObject = JsonConvert.SerializeObject(objectToWrite);
                w.WriteLine(stringObject);
            }
        }
    }
}
