namespace PingApp.Models
{
    public class PingHistory
    {
        public int Id { get; set; }

        // Relacja wiele-1 z urządzeniem
        public int DeviceId { get; set; }
        public Device? Device { get; set; }

        public DateTime Date { get; set; }
        public string Status { get; set; } = string.Empty;
    }

}
