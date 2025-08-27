using Microsoft.AspNetCore.Http;
using TechFood.Domain.Entities;

namespace TechFood.Domain.Interfaces.Gateway
{
    public interface IProductGateway
    {
        Task<Product?> GetByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task AddAsync(Product entity);
        Task SaveImageAsync(IFormFile file, string fileName);
        Task DeleteImageAsync(Product category);
        Task UpdateAsync(Product category);
        Task DeleteAsync(Product category);
    }
}
