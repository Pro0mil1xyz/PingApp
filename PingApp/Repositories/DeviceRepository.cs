namespace PingApp.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using PingApp.Data;
    using PingApp.Models;

    public class DeviceRepository : IDeviceRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _context;

        public DeviceRepository(IDbContextFactory<ApplicationDbContext> context)
        {
            _context = context;
        }

        // Pobierz wszystkie urządzenia z kategoriami i grupami, posortowane według nazwy
        public async Task<IEnumerable<Device>> GetAllDevicesAsync()
        {
            using (var dbContext = _context.CreateDbContext())
            {
                return await dbContext.Devices
                    .Include(d => d.Category)
                    .Include(d => d.Group)
                    .OrderBy(d => d.Name) // Opcjonalne sortowanie
                    .ToListAsync();
            }
        }

        // Pobierz jedno urządzenie po ID z relacjami do kategorii i grupy
        public async Task<Device?> GetDeviceByIdAsync(int id)
        {
            using (var dbContext = _context.CreateDbContext())
            {
                return await dbContext.Devices
                    .Include(d => d.Category)
                    .Include(d => d.Group)
                    .Where(d => d.Id == id)
                    .FirstOrDefaultAsync();
            }
        }

        // Dodaj nowe urządzenie
        public async Task AddDeviceAsync(Device device)
        {
            using (var dbContext = _context.CreateDbContext())
            {
                dbContext.Devices.Add(device);
                await dbContext.SaveChangesAsync();
            }
        }

        // Zaktualizuj istniejące urządzenie
        public async Task UpdateDeviceAsync(Device device)
        {
            using (var dbContext = _context.CreateDbContext())
            {
                var existingDevice = await dbContext.Devices
                    .Where(d => d.Id == device.Id)
                    .FirstOrDefaultAsync();

                if (existingDevice != null)
                {
                    dbContext.Entry(existingDevice).CurrentValues.SetValues(device);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        // Usuń urządzenie po ID
        public async Task DeleteDeviceAsync(int id)
        {
            using (var dbContext = _context.CreateDbContext())
            {
                var device = await dbContext.Devices
                    .Where(d => d.Id == id)
                    .FirstOrDefaultAsync();

                if (device != null)
                {
                    dbContext.Devices.Remove(device);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        // Pobierz wszystkie urządzenia należące do danej grupy
        public async Task<IEnumerable<Device>> GetDevicesByGroupIdAsync(int groupId)
        {
            using (var dbContext = _context.CreateDbContext())
            {
                return await dbContext.Devices
                    .Where(d => d.GroupId == groupId)
                    .Include(d => d.Category) // Opcjonalne
                    .Include(d => d.Group) // Opcjonalne
                    .ToListAsync();
            }
        }

        // Pobierz wszystkie urządzenia należące do danej kategorii
        public async Task<IEnumerable<Device>> GetDevicesByCategoryIdAsync(int categoryId)
        {
            using (var dbContext = _context.CreateDbContext())
            {
                return await dbContext.Devices
                    .Where(d => d.CategoryId == categoryId)
                    .Include(d => d.Category) // Opcjonalne
                    .Include(d => d.Group) // Opcjonalne
                    .ToListAsync();
            }
        }
    }
}