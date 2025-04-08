namespace PingApp.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using PingApp.Data;
    using PingApp.Models;

    public class DeviceGroupRepository : IDeviceGroupRepository
    {
        private readonly ApplicationDbContext _context;

        public DeviceGroupRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Pobierz wszystkie grupy urządzeń z opcjonalnym sortowaniem
        public async Task<IEnumerable<DeviceGroup>> GetAllDeviceGroupsAsync()
        {
            return await _context.DeviceGroups
                .OrderBy(g => g.GroupName) // Sortowanie po nazwie grupy (opcjonalne)
                .ToListAsync();
        }

        // Pobierz jedną grupę urządzeń po ID
        public async Task<DeviceGroup?> GetDeviceGroupByIdAsync(int id)
        {
            return await _context.DeviceGroups
                .Where(g => g.Id == id)
                .FirstOrDefaultAsync();
        }

        // Dodaj nową grupę urządzeń
        public async Task AddDeviceGroupAsync(DeviceGroup deviceGroup)
        {
            _context.DeviceGroups.Add(deviceGroup);
            await _context.SaveChangesAsync();
        }

        // Zaktualizuj istniejącą grupę urządzeń
        public async Task UpdateDeviceGroupAsync(DeviceGroup deviceGroup)
        {
            var existingGroup = await _context.DeviceGroups
                .Where(g => g.Id == deviceGroup.Id)
                .FirstOrDefaultAsync();

            if (existingGroup != null)
            {
                _context.Entry(existingGroup).CurrentValues.SetValues(deviceGroup);
                await _context.SaveChangesAsync();
            }
        }

        // Usuń grupę urządzeń po ID
        public async Task DeleteDeviceGroupAsync(int id)
        {
            var deviceGroup = await _context.DeviceGroups
                .Where(g => g.Id == id)
                .FirstOrDefaultAsync();

            if (deviceGroup != null)
            {
                _context.DeviceGroups.Remove(deviceGroup);
                await _context.SaveChangesAsync();
            }
        }
    }
}