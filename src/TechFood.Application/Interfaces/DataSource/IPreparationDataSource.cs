using TechFood.Common.DTO;

namespace TechFood.Application.Interfaces.DataSource
{
    public interface IPreparationDataSource
    {
        Task<Guid> AddAsync(PreparationDTO preparation);

        Task<PreparationDTO?> GetByIdAsync(Guid id);

        Task<PreparationDTO?> GetByOrderIdAsync(Guid orderId);

        Task<IEnumerable<PreparationDTO>> GetAllAsync();

        Task UpdateAsync(PreparationDTO preparation);

    }
}
