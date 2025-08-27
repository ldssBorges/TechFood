using TechFood.Application.Gateway;
using TechFood.Application.Interfaces.Controller;
using TechFood.Application.Interfaces.DataSource;
using TechFood.Application.Presenters;
using TechFood.Domain.Enums;
using TechFood.Domain.Interfaces.UseCase;
using TechFood.Domain.UseCases;
using static TechFood.Application.Controllers.PreparationMonitorPresenter;

namespace TechFood.Application.Controllers;

public class PreparationController : IPreparationController
{
    private readonly IPreparationUseCase _preparationUseCase;
    private readonly IProductUseCase _productUseCase;
    private readonly IOrderUseCase _orderUseCase;

    public PreparationController(
        IPreparationDataSource preparationDataSource,
        IOrderDataSource orderDataSource,
        IProductDataSource productDataSource,
        ICategoryDataSource categoryDataSource,
        IImageDataSource imageDataSource,
        IUnitOfWorkDataSource unitOfWork
        )
    {
        var preparationGateway = new PreparationGateway(preparationDataSource, unitOfWork);
        var orderGateway = new OrderGateway(orderDataSource, unitOfWork);
        var productGateway = new ProductGateway(productDataSource, unitOfWork);
        var categoryGateway = new CategoryGateway(categoryDataSource, imageDataSource, unitOfWork);

        _productUseCase = new ProductUseCase(productGateway, categoryGateway);
        _orderUseCase = new OrderUseCase(orderGateway, productGateway, preparationGateway);
        _preparationUseCase = new PreparationUseCase(preparationGateway);

    }


    public async Task<IEnumerable<PreparationMonitorPresenter>> GetAllPreparationOrdersAsync()
    {
        var preparations = await _preparationUseCase.GetAllAsync();

        var products = await _productUseCase.ListAllAsync();

        var preparationsMonitor = new List<PreparationMonitorPresenter>();

        foreach (var item in preparations)
        {
            var orderItem = await _orderUseCase.GetOrderByIdAsync(item.OrderId);

            var productMonitorPresenter = new List<ProductMonitorPresenter>();

            foreach (var itemProduct in orderItem.Items)
            {
                var productItem = products.FirstOrDefault(x => x.Id == itemProduct.ProductId);

                productMonitorPresenter.Add(
                    new ProductMonitorPresenter(productItem.Name, productItem.ImageFileName, itemProduct.Quantity));
            }

            var preparationMonitor = new PreparationMonitorPresenter()
            {
                Number = item.Number,
                Status = item.Status,
                OrderId = item.OrderId,
                preparationId = item.Id,
                Products = productMonitorPresenter
            };

            preparationsMonitor.Add(preparationMonitor);
        }

        return preparationsMonitor.OrderByDescending(x => x.Status);
    }

    public async Task<PreparationPresenter> GetPreparationByOrderIdAsync(Guid orderId)
    {
        var result = await _preparationUseCase.GetPreparationByOrderIdAsync(orderId);
        return result is not null ?
            new PreparationPresenter(
                result.Id,
                result.Status,
                result.CreatedAt,
                result.StartedAt,
                result.FinishedAt,
                result.OrderId
            ) : null;
    }

    public async Task<IEnumerable<PreparationPresenter>> GetAllAsync()
    {
        var preparations = await _preparationUseCase.GetAllAsync();

        return preparations.Select(preparation => new PreparationPresenter(
            preparation.Id,
            preparation.Status,
            preparation.CreatedAt,
            preparation.StartedAt,
            preparation.FinishedAt,
            preparation.OrderId
        ));
    }

    public async Task StartAsync(Guid id)
    {
        await _preparationUseCase.StartAsync(id);
    }

    public async Task FinishAsync(Guid id)
    {
        await _preparationUseCase.FinishAsync(id);
    }

    public async Task CancelAsync(Guid id)
    {
        await _preparationUseCase.CancelAsync(id);
    }
}


public class PreparationMonitorPresenter
{
    public PreparationMonitorPresenter()
    {
        Products = new();
    }

    public Guid preparationId { get; set; }

    public Guid OrderId { get; set; }

    public int Number { get; set; }

    public PreparationStatusType Status { get; set; }

    public List<ProductMonitorPresenter> Products { get; set; }

    public class ProductMonitorPresenter
    {
        public ProductMonitorPresenter(string? imageUrl, string name, int quantity)
        {
            ImageUrl = imageUrl;
            Name = name;
            Quantity = quantity;
        }

        public string? ImageUrl { get; set; }

        public string Name { get; set; } = null!;

        public int Quantity { get; set; }

        public void SetImage(string image)
            => ImageUrl = image;
    }
}
