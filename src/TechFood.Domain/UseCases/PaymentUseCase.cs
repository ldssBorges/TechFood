using TechFood.Common.DTO.Payment;
using TechFood.Domain.Entities;
using TechFood.Domain.Enums;
using TechFood.Domain.Interfaces.Gateway;
using TechFood.Domain.Interfaces.UseCase;

namespace TechFood.Domain.UseCases
{
    public class PaymentUseCase : IPaymentUseCase
    {
        private readonly IPaymentGateway _paymentGateway;
        private readonly IOrderGateway _orderGateway;
        private readonly IProductGateway _productGateway;
        private readonly IPaymentServiceGateway _paymentServiceGateway;
        private readonly IPreparationGateway _preparationGateway;
        private readonly IOrderNumberServiceGateway _orderNumberServiceGateway;

        public PaymentUseCase(IPaymentGateway paymentGateway,
                              IOrderGateway orderGateway,
                              IProductGateway productGateway,
                              IPaymentServiceGateway paymentServiceGateway,
                              IPreparationGateway preparationGateway,
                              IOrderNumberServiceGateway orderNumberServiceGateway)
        {
            _paymentGateway = paymentGateway;
            _orderGateway = orderGateway;
            _productGateway = productGateway;
            _paymentServiceGateway = paymentServiceGateway;
            _preparationGateway = preparationGateway;
            _orderNumberServiceGateway = orderNumberServiceGateway;
        }

        public async Task<int> ConfirmAsync(Guid id)
        {
            var payment = await _paymentGateway.GetByIdAsync(id);

            if (payment == null)
                throw new ApplicationException("Payment not found.");

            var order = await _orderGateway.GetByIdAsync(payment.OrderId);

            if (order == null)
                throw new ApplicationException("Order not found.");

            payment.Confirm();

            order.ConfirmPayment();


            await _orderGateway.UpdateAsync(order);

            await _paymentGateway.UpdateAsync(payment);

            var preparation = new Preparation(payment.OrderId, payment.Number);

            await _preparationGateway.AddAsync(preparation);

            return preparation.Number;
        }

        public async Task<Payment?> CreateAsync(CreatePaymentRequestDTO data)
        {
            var order = await _orderGateway.GetByIdAsync(data.OrderId!.Value);

            if (order == null)
            {
                return null;
            }

            var payment = new Payment(data.OrderId!.Value, (PaymentType)data.Type, order.Amount);

            var products = (await _productGateway.GetAllAsync()).ToList();

            if (payment.Type == PaymentType.MercadoPago)
            {
                var paymentResult = await _paymentServiceGateway.GenerateQrCodePaymentAsync(products, order);
                payment.SetQRCodeData(paymentResult.QrCodeData);
            }
            else if (payment.Type == PaymentType.CreditCard)
            {
                // TODO: Implement credit card payment
                throw new NotImplementedException("Credit card payment is not implemented yet.");
            }

            payment.SetOrderNumber(await _orderNumberServiceGateway.GetAsync());

            order.CreatePayment();

            await _paymentGateway.AddAsync(payment);

            await _orderGateway.UpdateAsync(order);

            return payment;
        }

        public async Task<Payment?> GetByIdAsync(Guid id)
            => await _paymentGateway.GetByIdAsync(id);

        public async Task<Payment?> GetByOrderIdAsync(Guid id)
            => await _paymentGateway.GetByOrderIdAsync(id);
    }
}
