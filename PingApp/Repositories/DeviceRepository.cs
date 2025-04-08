namespace PingApp.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using PingApp.Data; // Namespace Twojego ApplicationDbContext
    using PingApp.Models;

    public class DeviceRepository : IDeviceRepository
    {
        private readonly ApplicationDbContext _context;

        public DeviceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Device>> GetAllDevicesAsync()
        {
            return await _context.Devices
                .Include(d => d.Category)
                .Include(d => d.Group)
                .ToListAsync();
        }

        public async Task<Device?> GetDeviceByIdAsync(int id)
        {
            return await _context.Devices
                .Include(d => d.Category)
                .Include(d => d.Group)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task AddDeviceAsync(Device device)
        {
            _context.Devices.Add(device);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDeviceAsync(Device device)
        {
            _context.Devices.Update(device);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDeviceAsync(int id)
        {
            var device = await GetDeviceByIdAsync(id);
            if (device != null)
            {
                _context.Devices.Remove(device);
                await _context.SaveChangesAsync();
            }
        }
    }
}
