using Microsoft.EntityFrameworkCore;
using TechFood.Application.Interfaces.DataSource;
using TechFood.Common.DTO;
using TechFood.Infra.Data.Contexts;

namespace TechFood.Infra.Data.Repositories;

public class ProductRepository(TechFoodContext dbContext) : IProductDataSource
{
    private readonly DbSet<ProductDTO> _products = dbContext.Products;
    private readonly DbSet<CategoryDTO> _categories = dbContext.Categories;

    public async Task<IEnumerable<ProductDTO>> GetAllAsync()
        => await _products.AsNoTracking().ToListAsync();

    public async Task<ProductDTO?> GetByIdAsync(Guid id)
        => await _products.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();

    public async Task<Guid> AddAsync(ProductDTO product)
    {
        var session = await _products.AddAsync(product);

        return session.Entity.Id;
    }

    public async Task DeleteAsync(ProductDTO product)
        => await Task.FromResult(_products.Remove(product));

    public async Task UpdateAsync(ProductDTO product)
         => await Task.FromResult(_products.Update(product));

    public async Task<CategoryDTO?> GetCategoryByIdAsync(Guid categoryId)
        => await _categories.Where(x => x.Id == categoryId).AsNoTracking().FirstOrDefaultAsync();

}
