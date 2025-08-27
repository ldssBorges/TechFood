using TechFood.Application.Interfaces.DataSource;
using TechFood.Infra.Data.Contexts;

namespace TechFood.Infra.Data.Repositories;

public class AnotherUnitOfWork(TechFoodContext dbContext) : IUnitOfWorkDataSource
{
    private readonly TechFoodContext _context = dbContext;

    public async Task<bool> CommitAsync()
    {
        var success = await _context.SaveChangesAsync() > 0;
        return success;
    }

    public Task RollbackAsync()
    {
        return Task.CompletedTask;
    }
}
