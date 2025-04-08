namespace PingApp.Models
{
    public class Device
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string IPAddress { get; set; } = string.Empty;

        // Relacja wiele-1 z kategorią
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        // Relacja wiele-1 z grupą urządzeń
        public int GroupId { get; set; }
        public DeviceGroup? Group { get; set; }

        // Relacja 1-wiele z historią pingów
        public ICollection<PingHistory> PingHistories { get; set; } = new List<PingHistory>();
    }

}
