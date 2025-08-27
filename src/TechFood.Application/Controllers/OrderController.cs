using TechFood.Application.Gateway;
using TechFood.Application.Interfaces.Controller;
using TechFood.Application.Interfaces.DataSource;
using TechFood.Application.Presenters;
using TechFood.Common.DTO;
using TechFood.Domain.Interfaces.UseCase;
using TechFood.Domain.UseCases;

namespace TechFood.Application.Controllers;

public class OrderController : IOrderController
{
    private readonly IOrderUseCase _orderUseCase;

    public OrderController(
        IOrderDataSource orderDataSource,
        IProductDataSource productDataSource,
        IPreparationDataSource preparationDataSource,
        IUnitOfWorkDataSource unitOfWork
        )
    {
        var orderGateway = new OrderGateway(orderDataSource, unitOfWork);
        var productGateway = new ProductGateway(productDataSource, unitOfWork);
        var preparationGateway = new PreparationGateway(preparationDataSource, unitOfWork);
        _orderUseCase = new OrderUseCase(orderGateway, productGateway, preparationGateway);
    }

    public async Task<CreateOrderPresenter?> CreateOrderAsync(CreateOrderRequestDTO request)
    {

        var order = await _orderUseCase.CreateOrderAsync(request);

        return order is not null ?
                   CreateOrderPresenter.Create(order) :
                   null;
    }

    public async Task FinishAsync(FinishOrderRequestDTO request)
    {
        await _orderUseCase.FinishAsync(request);

        return;
    }

    public async Task<IEnumerable<OrderPresenter>> GetAllDoneAndInPreparationAsync()
    {
        var orders = await _orderUseCase.GetAllDoneAndInPreparationAsync();

        return orders.Any() ?
            orders.Select(x => OrderPresenter.Create(x)):
                 null;
    }
}
