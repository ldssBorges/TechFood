using TechFood.Domain.Entities;

namespace TechFood.Domain.Interfaces.Gateway
{

    public interface IPaymentServiceGateway
    {
        Task<QrCodePayment> GenerateQrCodePaymentAsync(IEnumerable<Product> product, Order order);
    }

}
