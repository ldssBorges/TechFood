using TechFood.Common.DTO;
using TechFood.Domain.Entities;

namespace TechFood.Domain.Interfaces.UseCase;

public interface IOrderUseCase
{
    Task<Order> CreateOrderAsync(CreateOrderRequestDTO request);

    Task<Order> GetOrderByIdAsync(Guid orderId);

    Task FinishAsync(FinishOrderRequestDTO request);

    Task<IEnumerable<Order>> GetAllDoneAndInPreparationAsync();
}
