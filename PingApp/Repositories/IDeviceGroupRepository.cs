using PingApp.Models;

namespace PingApp.Repositories
{
    public interface IDeviceGroupRepository
    {
        Task<IEnumerable<DeviceGroup>> GetAllDeviceGroupsAsync();
        Task<DeviceGroup?> GetDeviceGroupByIdAsync(int id);
        Task AddDeviceGroupAsync(DeviceGroup deviceGroup);
        Task UpdateDeviceGroupAsync(DeviceGroup deviceGroup);
        Task DeleteDeviceGroupAsync(int id);
    }

}
