namespace PingApp.Models
{
    public class DeviceGroup
    {
        public int Id { get; set; }
        public string GroupName { get; set; } = string.Empty;

        // Relacja 1-wiele z urządzeniami
        public ICollection<Device> Devices { get; set; } = new List<Device>();
    }

}
