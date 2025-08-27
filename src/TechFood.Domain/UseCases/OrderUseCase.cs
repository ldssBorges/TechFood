using TechFood.Common.DTO;
using TechFood.Domain.Entities;
using TechFood.Domain.Interfaces.Gateway;
using TechFood.Domain.Interfaces.UseCase;

namespace TechFood.Domain.UseCases;

public class OrderUseCase : IOrderUseCase
{
    private readonly IOrderGateway _orderGateway;
    private readonly IProductGateway _productGateway;
    private readonly IPreparationGateway _preparationGateway;

    public OrderUseCase(IOrderGateway orderGateway,
        IProductGateway productGateway,
        IPreparationGateway preparationGateway)
    {
        _orderGateway = orderGateway;
        _productGateway = productGateway;
        _preparationGateway = preparationGateway;
    }

    public async Task<Order> CreateOrderAsync(CreateOrderRequestDTO request)
    {
        var products = await _productGateway.GetAllAsync();

        var orderItems = request.Items
            .Select(i =>
            {
                var product = products.First(p => p!.Id == i.ProductId)!;
                return new OrderItem(product.Id, product.Price, i.Quantity);
            });

        var order = new Order(request.CustomerId);

        foreach (var item in orderItems)
           order.AddItem(item);

        var orderId = await _orderGateway.AddAsync(order);

        order.SetId(orderId);

        return order;
    }

    public async Task<Order> GetOrderByIdAsync(Guid orderId)
    {
        var order = await _orderGateway.GetByIdAsync(orderId);

        if (order == null)
            throw new ApplicationException("Order not found.");

        return order;
    }

    public async Task FinishAsync(FinishOrderRequestDTO request)
    {
        var order = await _orderGateway.GetByIdAsync(request.OrderId);

        if (order == null)
            throw new ApplicationException("Order not found.");

        var preparation = await _preparationGateway.GetByOrderIdAsync(request.OrderId);

        if (preparation == null)
            throw new ApplicationException("Preparation not found.");

        preparation.Delivered();

        order.Finish();

        await _preparationGateway.UpdateAsync(preparation);

        await _orderGateway.UpdateAsync(order);
    }

    public async Task<IEnumerable<Order>> GetAllDoneAndInPreparationAsync()
    {
       return await _orderGateway.GetAllDoneAndInPreparationAsync();
    }
}
