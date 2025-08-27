using Microsoft.EntityFrameworkCore;
using TechFood.Application.Interfaces.DataSource;
using TechFood.Common.DTO;
using TechFood.Infra.Data.Contexts;

namespace TechFood.Infra.Data.Repositories;

public class CategoryRepository(TechFoodContext dbContext) : ICategoryDataSource
{
    private readonly DbSet<CategoryDTO> _categories = dbContext.Categories;

    public async Task<Guid> AddAsync(CategoryDTO entity)
    {
        var result = await _categories.AddAsync(entity);

        return result.Entity.Id;
    }

    public async Task UpdateAsync(CategoryDTO category)
        => await Task.FromResult(_categories.Update(category));

    public async Task DeleteAsync(CategoryDTO category)
        => await Task.FromResult(_categories.Remove(category));

    public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
        => await _categories.AsNoTracking().OrderBy(c => c.SortOrder).ToListAsync();

    public async Task<CategoryDTO?> GetByIdAsync(Guid id)
        => await _categories.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();

}
