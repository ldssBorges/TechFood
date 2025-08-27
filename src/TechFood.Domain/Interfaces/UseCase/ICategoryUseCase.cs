using TechFood.Common.DTO.Category;
using TechFood.Domain.Entities;

namespace TechFood.Domain.Interfaces.UseCase
{
    public interface ICategoryUseCase
    {
        Task<IEnumerable<Category>> ListAllAsync();

        Task<Category?> GetByIdAsync(Guid id);

        Task<Category> AddAsync(CreateCategoryRequestDTO category, string fileName);

        Task<Category?> UpdateAsync(Guid id, UpdateCategoryRequestDTO category, string fileName);

        Task<bool> DeleteAsync(Guid id);
    }
}
