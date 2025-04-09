using PingApp.Models;

namespace PingApp.Repositories
{
    public interface IPingRepository
    {
        Task<string> PingDeviceAsync(string ipAddress);
        Task SavePingResultAsync(string ipAddress, int deviceId);
        Task<IEnumerable<PingHistory>> GetAllPingHistoriesAsync(); // Nowa metoda do pobierania historii pingów
        Task DeleteAllPingHistoriesAsync();
    }
}