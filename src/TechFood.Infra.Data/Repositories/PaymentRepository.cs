using Microsoft.EntityFrameworkCore;
using TechFood.Application.Interfaces.DataSource;
using TechFood.Common.DTO;
using TechFood.Domain.Entities;
using TechFood.Infra.Data.Contexts;

namespace TechFood.Infra.Data.Repositories;

public class PaymentRepository(TechFoodContext dbContext) : IPaymentDataSource
{
    private readonly DbSet<PaymentDTO> _payments = dbContext.Payments;

    public async Task<Guid> AddAsync(PaymentDTO payment)
    {
        var entry = await _payments.AddAsync(payment);

        return entry.Entity.Id;
    }

    public Task<PaymentDTO?> GetByIdAsync(Guid id)
    {
        return _payments
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateAsync(PaymentDTO payment)
    {
        await Task.FromResult(_payments.Update(payment));
    }

    public Task<PaymentDTO?> GetByOrderIdAsync(Guid id)
    {
        return _payments
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.OrderId == id);
    }
}
