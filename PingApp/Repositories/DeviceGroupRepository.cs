namespace PingApp.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using PingApp.Data;
    using PingApp.Models;

    public class DeviceGroupRepository : IDeviceGroupRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _context;

        public DeviceGroupRepository(IDbContextFactory<ApplicationDbContext> context)
        {
            _context = context;
        }

        // Pobierz wszystkie grupy urządzeń z opcjonalnym sortowaniem
        public async Task<IEnumerable<DeviceGroup>> GetAllDeviceGroupsAsync()
        {
            using (var dbContext = _context.CreateDbContext())
            {
                return await dbContext.DeviceGroups
                .OrderBy(g => g.GroupName) // Sortowanie po nazwie grupy (opcjonalne)
                .ToListAsync();
            }
        }

        // Pobierz jedną grupę urządzeń po ID
        public async Task<DeviceGroup?> GetDeviceGroupByIdAsync(int id)
        {
            using (var dbContext = _context.CreateDbContext())
            {
                return await dbContext.DeviceGroups
                .Where(g => g.Id == id)
                .FirstOrDefaultAsync();
            }
        
        }

        // Dodaj nową grupę urządzeń
        public async Task AddDeviceGroupAsync(DeviceGroup deviceGroup)
        {
            using (var dbContext = _context.CreateDbContext())
            {
                dbContext.DeviceGroups.Add(deviceGroup);
                await dbContext.SaveChangesAsync();
            }
        }

        // Zaktualizuj istniejącą grupę urządzeń
        public async Task UpdateDeviceGroupAsync(DeviceGroup deviceGroup)
        {
            using (var dbContext = _context.CreateDbContext())
            {
                var existingGroup = await dbContext.DeviceGroups
                .Where(g => g.Id == deviceGroup.Id)
                .FirstOrDefaultAsync();

                if (existingGroup != null)
                {
                    dbContext.Entry(existingGroup).CurrentValues.SetValues(deviceGroup);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        // Usuń grupę urządzeń po ID
        public async Task DeleteDeviceGroupAsync(int id)
        {
            using (var dbContext = _context.CreateDbContext())
            {
                var deviceGroup = await dbContext.DeviceGroups
                .Where(g => g.Id == id)
                .FirstOrDefaultAsync();

                if (deviceGroup != null)
                {
                    dbContext.DeviceGroups.Remove(deviceGroup);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}