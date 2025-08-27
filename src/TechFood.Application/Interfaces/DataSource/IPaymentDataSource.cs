using TechFood.Common.DTO;

namespace TechFood.Application.Interfaces.DataSource
{
    public interface IPaymentDataSource
    {
        Task<Guid> AddAsync(PaymentDTO payment);

        Task<PaymentDTO?> GetByIdAsync(Guid id);

        Task UpdateAsync(PaymentDTO payment);

        Task<PaymentDTO?> GetByOrderIdAsync(Guid id);
    }
}
