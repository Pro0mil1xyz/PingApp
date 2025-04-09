using System.Net.NetworkInformation;
using Microsoft.EntityFrameworkCore;
using PingApp.Data;
using PingApp.Models;

namespace PingApp.Repositories
{
    public class PingRepository : IPingRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _context;

        public PingRepository(IDbContextFactory<ApplicationDbContext> context)
        {
            _context = context;
        }

        // Pingowanie pojedynczego urządzenia
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

        // Zapisywanie wyniku pingowania w bazie
        public async Task SavePingResultAsync(string ipAddress, int deviceId)
        {
            using var ping = new Ping();
            var reply = await ping.SendPingAsync(ipAddress);

            using var dbContext = _context.CreateDbContext();

            // Sprawdzamy, czy DeviceId istnieje w tabeli Devices
            var deviceExists = deviceId > 0 && await dbContext.Devices.AnyAsync(d => d.Id == deviceId);

            var pingHistory = new PingHistory
            {
                IPAddress = ipAddress,
                DeviceId = deviceExists ? deviceId : (int?)null, // NULL, jeśli urządzenie nie istnieje
                Date = DateTime.UtcNow,
                Status = reply.Status.ToString()
            };

            if (reply.Status == IPStatus.Success)
            {
                pingHistory.Status += $" (Czas odpowiedzi: {reply.RoundtripTime} ms)";
            }

            try
            {
                dbContext.PingHistories.Add(pingHistory);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas zapisywania historii pingów: {ex.Message}");
                // Rozważ użycie loggera zamiast Console.WriteLine
            }
        }

        // Pobieranie wszystkich zapisanych pingów z bazy
        public async Task<IEnumerable<PingHistory>> GetAllPingHistoriesAsync()
        {
            using var dbContext = _context.CreateDbContext();
            return await dbContext.PingHistories
                .Include(ph => ph.Device) // Pobranie powiązanego urządzenia
                .OrderByDescending(ph => ph.Date) // Sortowanie od najnowszych wyników
                .ToListAsync();
        }

        public async Task DeleteAllPingHistoriesAsync()
        {
            using var dbContext = _context.CreateDbContext();
            dbContext.PingHistories.RemoveRange(dbContext.PingHistories); // Usuwanie wszystkich rekordów
            await dbContext.SaveChangesAsync();
        }
    }
}