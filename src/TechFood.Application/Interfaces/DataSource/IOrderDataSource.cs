using TechFood.Common.DTO;

namespace TechFood.Application.Interfaces.DataSource
{
    public interface IOrderDataSource
    {
        Task<Guid> AddAsync(OrderDTO order);
        Task<OrderDTO?> GetByIdAsync(Guid id);
        Task<List<OrderDTO>> GetAllDoneAndInPreparationAsync();
        Task UpdateAsync(OrderDTO order);
    }
}
