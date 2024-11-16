namespace IoTDashboard
{
    public class Device
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DeviceState State { get; set; }
    }

    public enum DeviceState
    {
        Unknown,
        Running,
        Stopped
    }
}
