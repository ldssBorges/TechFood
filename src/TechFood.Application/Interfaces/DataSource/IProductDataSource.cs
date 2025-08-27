using TechFood.Common.DTO;

namespace TechFood.Application.Interfaces.DataSource
{
    public interface IProductDataSource : IDataSource<ProductDTO>
    {
        Task<CategoryDTO?> GetCategoryByIdAsync(Guid categoryId);
    }
}
