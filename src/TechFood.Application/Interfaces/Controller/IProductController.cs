using TechFood.Application.Presenters;
using TechFood.Common.DTO.Product;

namespace TechFood.Application.Interfaces.Controller
{
    public interface IProductController
    {
        Task<IEnumerable<ProductPresenter>> ListAllAsync();

        Task<ProductPresenter?> GetByIdAsync(Guid id);

        Task<ProductPresenter?> AddAsync(CreateProductRequestDTO product);

        Task<ProductPresenter?> UpdateAsync(Guid id, UpdateProductRequestDTO product);

        Task<bool> DeleteAsync(Guid id);
    }
}
