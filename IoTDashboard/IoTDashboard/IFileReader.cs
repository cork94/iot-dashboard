namespace IoTDashboard
{
    public interface IFileReader
    {
        T DecerializeFromFile<T>(string filePath) where T : new();
    }
}