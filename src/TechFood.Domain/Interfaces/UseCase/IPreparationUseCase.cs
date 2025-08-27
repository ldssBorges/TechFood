using TechFood.Domain.Entities;

namespace TechFood.Domain.Interfaces.UseCase;

public interface IPreparationUseCase
{
    Task<Preparation> GetPreparationByOrderIdAsync(Guid orderId);

    Task<IEnumerable<Preparation>> GetAllAsync();

    Task<Preparation> GetByIdAsync(Guid id);

    Task StartAsync(Guid id);

    Task FinishAsync(Guid id);

    Task CancelAsync(Guid id);
}
