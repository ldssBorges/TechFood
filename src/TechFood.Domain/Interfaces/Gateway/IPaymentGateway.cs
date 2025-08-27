using TechFood.Domain.Entities;

namespace TechFood.Domain.Interfaces.Gateway
{
    public interface IPaymentGateway
    {
        Task<Guid> AddAsync(Payment payment);

        Task<Payment?> GetByIdAsync(Guid id);

        Task UpdateAsync(Payment product);

        Task<Payment?> GetByOrderIdAsync(Guid id);
    }
}
