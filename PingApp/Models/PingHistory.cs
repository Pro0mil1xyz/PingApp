namespace PingApp.Models
{
    public class PingHistory
    {
        public int Id { get; set; }
        public string IPAddress { get; set; } = string.Empty; // Nowa właściwość
        public int? DeviceId { get; set; } // Pozostaw jako opcjonalne, jeśli chcesz
        public Device? Device { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; } = string.Empty;
    }

}
