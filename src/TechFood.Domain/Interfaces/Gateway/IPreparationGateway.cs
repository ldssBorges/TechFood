using TechFood.Domain.Entities;

namespace TechFood.Domain.Interfaces.Gateway;

public interface IPreparationGateway
{
    Task<Guid> AddAsync(Preparation preparation);

    Task<Preparation?> GetByIdAsync(Guid id);

    Task<Preparation?> GetByOrderIdAsync(Guid orderId);

    Task<IEnumerable<Preparation>> GetAllAsync();

    Task UpdateAsync(Preparation preparation);
}
