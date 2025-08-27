using TechFood.Application.Interfaces.Service;
using TechFood.Domain.Interfaces.Gateway;

namespace TechFood.Application.Gateway
{
    public class OrderNumberServiceGateway : IOrderNumberServiceGateway
    {
        private readonly IOrderNumberService _orderNumberService;
        public OrderNumberServiceGateway(IOrderNumberService orderNumberService)
        {
            _orderNumberService = orderNumberService;
        }
        public Task<int> GetAsync()
        {
            return _orderNumberService.GetAsync();
        }
    }
}
