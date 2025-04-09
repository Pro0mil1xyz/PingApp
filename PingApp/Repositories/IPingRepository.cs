using PingApp.Models;

namespace PingApp.Repositories
{
    public interface IPingRepository
    {
        Task<string> PingDeviceAsync(string ipAddress);
        Task SavePingResultAsync(string ipAddress, int deviceId);
    }

}
