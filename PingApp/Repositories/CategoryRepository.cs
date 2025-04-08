namespace PingApp.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using PingApp.Data;
    using PingApp.Models;

    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Pobierz wszystkie kategorie z opcjonalnym sortowaniem
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories
                .OrderBy(c => c.CategoryName) // Sortowanie po nazwie (opcjonalne)
                .ToListAsync();
        }

        // Pobierz jedną kategorię po ID z użyciem lambda
        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
        }

        // Dodaj nową kategorię
        public async Task AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        // Zaktualizuj istniejącą kategorię
        public async Task UpdateCategoryAsync(Category category)
        {
            var existingCategory = await _context.Categories
                .Where(c => c.Id == category.Id)
                .FirstOrDefaultAsync();

            if (existingCategory != null)
            {
                _context.Entry(existingCategory).CurrentValues.SetValues(category);
                await _context.SaveChangesAsync();
            }
        }

        // Usuń kategorię po ID
        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();

            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}