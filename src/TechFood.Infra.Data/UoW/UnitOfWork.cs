using TechFood.Application.Interfaces.DataSource;
using TechFood.Infra.Data.Contexts;

namespace TechFood.Infra.Data.UoW;

public class UnitOfWork(TechFoodContext dbContext) : IUnitOfWorkDataSource
{
    public async Task<bool> CommitAsync()
    {
        var success = await dbContext.SaveChangesAsync() > 0;
        return success;
    }

    public Task RollbackAsync()
    {
        return Task.CompletedTask;
    }
}
