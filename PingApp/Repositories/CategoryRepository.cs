namespace PingApp.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using PingApp.Data;
    using PingApp.Models;

    public class CategoryRepository : ICategoryRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _context;

        public CategoryRepository(IDbContextFactory<ApplicationDbContext> context)
        {
            _context = context;
        }

        // Pobierz wszystkie kategorie z opcjonalnym sortowaniem
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            using (var dbContext = _context.CreateDbContext())
            {
                return await dbContext.Categories
                    .OrderBy(c => c.CategoryName) // Sortowanie po nazwie (opcjonalne)
                    .ToListAsync();
            }
        }

        // Pobierz jedną kategorię po ID z użyciem lambda
        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            using (var dbContext = _context.CreateDbContext())
            {
                return await dbContext.Categories
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
            }
        }

        // Dodaj nową kategorię
        public async Task AddCategoryAsync(Category category)
        {
            using (var dbContext = _context.CreateDbContext())
            {
                dbContext.Categories.Add(category);
                await dbContext.SaveChangesAsync();
            }
        }

        // Zaktualizuj istniejącą kategorię
        public async Task UpdateCategoryAsync(Category category)
        {
            using (var dbContext = _context.CreateDbContext())
            {
                var existingCategory = await dbContext.Categories
                .Where(c => c.Id == category.Id)
                .FirstOrDefaultAsync();

                if (existingCategory != null)
                {
                    dbContext.Entry(existingCategory).CurrentValues.SetValues(category);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        // Usuń kategorię po ID
        public async Task DeleteCategoryAsync(int id)
        {
            using (var dbContext = _context.CreateDbContext())
            {
                var category = await dbContext.Categories
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();

                if (category != null)
                {
                    dbContext.Categories.Remove(category);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}