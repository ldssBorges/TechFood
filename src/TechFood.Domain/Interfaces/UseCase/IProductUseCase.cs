using TechFood.Common.DTO.Product;
using TechFood.Domain.Entities;

namespace TechFood.Domain.Interfaces.UseCase
{
    public interface IProductUseCase
    {
        Task<IEnumerable<Product>> ListAllAsync();

        Task<Product?> GetByIdAsync(Guid id);

        Task<Product> AddAsync(CreateProductRequestDTO productDTO, string fileName);

        Task<Product?> UpdateAsync(Guid id, UpdateProductRequestDTO productDTO, string fileName);

        Task<bool> DeleteAsync(Guid id);
    }
}
