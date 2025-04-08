namespace PingApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;

        // Relacja 1-wiele z urządzeniami
        public ICollection<Device> Devices { get; set; } = new List<Device>();
    }

}
