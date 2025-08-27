using TechFood.Application.Interfaces.Service;
using TechFood.Common.DTO.Payment;
using TechFood.Domain.Entities;
using TechFood.Domain.Interfaces.Gateway;

namespace TechFood.Application.Gateway
{
    public class PaymentServiceGateway : IPaymentServiceGateway
    {
        private readonly IPaymentService _paymentService;
        public PaymentServiceGateway(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        public async Task<QrCodePayment> GenerateQrCodePaymentAsync(IEnumerable<Product> products, Order order)
        {
            var qRCodePaymentRequestDTO = new QrCodePaymentRequestDTO(
                "TOTEM01",
                order.Id.ToString().Replace("-", ""),
                "TechFood - Order #" + order.Id,
                order.Amount,
                order.Items.ToList().ConvertAll(i => new PaymentItemDTO(
                    products.FirstOrDefault(p => p.Id == i.ProductId)?.Name ?? "",
                    i.Quantity,
                    "unit",
                    i.UnitPrice,
                    i.UnitPrice * i.Quantity))
            );

            var paymentResponse = await _paymentService.GenerateQrCodePaymentAsync(qRCodePaymentRequestDTO);

            return new QrCodePayment(paymentResponse.QrCodeId, paymentResponse.QrCodeData);
        }
    }
}
