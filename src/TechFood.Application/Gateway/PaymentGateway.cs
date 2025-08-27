using TechFood.Application.Interfaces.DataSource;
using TechFood.Common.DTO;
using TechFood.Common.DTO.Enums;
using TechFood.Domain.Entities;
using TechFood.Domain.Enums;
using TechFood.Domain.Interfaces.Gateway;

namespace TechFood.Application.Gateway
{
    public class PaymentGateway : IPaymentGateway
    {
        private IUnitOfWorkDataSource _unitOfWorkDataSource;
        private IPaymentDataSource _paymentDataSource;

        public PaymentGateway(IUnitOfWorkDataSource unitOfWorkDataSource,
                              IPaymentDataSource paymentDataSource)
        {
            _unitOfWorkDataSource = unitOfWorkDataSource;
            _paymentDataSource = paymentDataSource;
        }

        public async Task<Guid> AddAsync(Payment payment)
        {
            var paymentDto = new PaymentDTO
            {
                Id = payment.Id,
                OrderId = payment.OrderId,
                Type = (PaymentTypeDTO)payment.Type,
                Amount = payment.Amount,
                CreatedAt = payment.CreatedAt,
                IsDeleted = payment.IsDeleted,
                PaidAt = payment.PaidAt,
                Status = (PaymentStatusTypeDTO)payment.Status
            };

            var result = await _paymentDataSource.AddAsync(paymentDto);

            await _unitOfWorkDataSource.CommitAsync();

            return result;
        }

        public async Task<Payment?> GetByIdAsync(Guid id)
        {
            var payment = await _paymentDataSource.GetByIdAsync(id);

            return payment is not null
                ? new Payment(payment.OrderId, (PaymentType)payment.Type, payment.Amount, payment.Id, (PaymentStatusType)payment.Status)
                : null;
        }

        public async Task UpdateAsync(Payment payment)
        {
            var paymentDTO = new PaymentDTO
            {
                Amount = payment.Amount,
                Id = payment.Id,
                CreatedAt = payment.CreatedAt,
                IsDeleted = payment.IsDeleted,
                OrderId = payment.OrderId,
                PaidAt = payment.PaidAt,
                Status = (PaymentStatusTypeDTO)payment.Status,
                Type = (PaymentTypeDTO)payment.Type
            };

            await _paymentDataSource.UpdateAsync(paymentDTO);

            await _unitOfWorkDataSource.CommitAsync();
        }

        public async Task<Payment?> GetByOrderIdAsync(Guid id)
        {
            var payment = await _paymentDataSource.GetByOrderIdAsync(id);

            return payment is not null
                ? new Payment(payment.OrderId, (PaymentType)payment.Type, payment.Amount, payment.Id)
                : null;
        }
    }
}
