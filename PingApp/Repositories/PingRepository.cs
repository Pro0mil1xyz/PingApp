using System.Net.NetworkInformation;
using PingApp.Data;
using PingApp.Models;

namespace PingApp.Repositories
{
    public class PingRepository : IPingRepository
    {
        private readonly ApplicationDbContext _context;

        public PingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> PingDeviceAsync(string ipAddress)
        {
            using var ping = new Ping();
            try
            {
                var reply = await ping.SendPingAsync(ipAddress);

                if (reply.Status == IPStatus.Success)
                {
                    return $"Odpowiedź: {reply.RoundtripTime} ms (Status: {reply.Status})";
                }
                else
                {
                    return $"Błąd pingowania: {reply.Status}";
                }
            }
            catch (Exception ex)
            {
                return $"Błąd: {ex.Message}";
            }
        }

        public async Task SavePingResultAsync(string ipAddress, int deviceId)
        {
            using var ping = new Ping();
            var reply = await ping.SendPingAsync(ipAddress);

            var pingHistory = new PingHistory
            {
                DeviceId = deviceId,
                Date = DateTime.UtcNow,
                Status = reply.Status.ToString()
            };

            if (reply.Status == IPStatus.Success)
            {
                pingHistory.Status += $" (Czas odpowiedzi: {reply.RoundtripTime} ms)";
            }

            _context.PingHistories.Add(pingHistory);
            await _context.SaveChangesAsync();
        }
    }
}