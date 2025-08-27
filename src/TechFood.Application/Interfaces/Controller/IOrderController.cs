using TechFood.Application.Presenters;
using TechFood.Common.DTO;

namespace TechFood.Application.Interfaces.Controller;

public interface IOrderController
{
    Task<CreateOrderPresenter?> CreateOrderAsync(CreateOrderRequestDTO request);

    Task FinishAsync(FinishOrderRequestDTO request);

    Task<IEnumerable<OrderPresenter>> GetAllDoneAndInPreparationAsync();
}
