using Microsoft.AspNetCore.Http;
using TechFood.Domain.Entities;

namespace TechFood.Domain.Interfaces.Gateway
{
    public interface ICategoryGateway
    {
        Task<Category?> GetByIdAsync(Guid id);
        Task<IEnumerable<Category>> GetAllAsync();
        Task AddAsync(Category entity);
        Task SaveImageAsync(IFormFile file, string fileName);
        Task DeleteImageAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Category category);
    }
}
