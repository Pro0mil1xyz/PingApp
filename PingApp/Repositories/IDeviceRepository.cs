using PingApp.Models;

namespace PingApp.Repositories
{
    public interface IDeviceRepository
    {
        Task<IEnumerable<Device>> GetAllDevicesAsync();
        Task<Device?> GetDeviceByIdAsync(int id);
        Task AddDeviceAsync(Device device);
        Task UpdateDeviceAsync(Device device);
        Task DeleteDeviceAsync(int id);

        Task<IEnumerable<Device>> GetDevicesByCategoryIdAsync(int categoryId);
        Task<IEnumerable<Device>> GetDevicesByGroupIdAsync(int groupId);

    }
}
