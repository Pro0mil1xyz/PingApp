namespace PingApp.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using PingApp.Data;
    using PingApp.Models;

    public class DeviceRepository : IDeviceRepository
    {
        private readonly ApplicationDbContext _context;

        public DeviceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Pobierz wszystkie urządzenia z kategoriami i grupami, posortowane według nazwy
        public async Task<IEnumerable<Device>> GetAllDevicesAsync()
        {
            return await _context.Devices
                .Include(d => d.Category)
                .Include(d => d.Group)
                .OrderBy(d => d.Name) // Opcjonalne sortowanie
                .ToListAsync();
        }

        // Pobierz jedno urządzenie po ID z relacjami do kategorii i grupy
        public async Task<Device?> GetDeviceByIdAsync(int id)
        {
            return await _context.Devices
                .Include(d => d.Category)
                .Include(d => d.Group)
                .Where(d => d.Id == id)
                .FirstOrDefaultAsync();
        }

        // Dodaj nowe urządzenie
        public async Task AddDeviceAsync(Device device)
        {
            _context.Devices.Add(device);
            await _context.SaveChangesAsync();
        }

        // Zaktualizuj istniejące urządzenie
        public async Task UpdateDeviceAsync(Device device)
        {
            var existingDevice = await _context.Devices
                .Where(d => d.Id == device.Id)
                .FirstOrDefaultAsync();

            if (existingDevice != null)
            {
                _context.Entry(existingDevice).CurrentValues.SetValues(device);
                await _context.SaveChangesAsync();
            }
        }

        // Usuń urządzenie po ID
        public async Task DeleteDeviceAsync(int id)
        {
            var device = await _context.Devices
                .Where(d => d.Id == id)
                .FirstOrDefaultAsync();

            if (device != null)
            {
                _context.Devices.Remove(device);
                await _context.SaveChangesAsync();
            }
        }
    }
}