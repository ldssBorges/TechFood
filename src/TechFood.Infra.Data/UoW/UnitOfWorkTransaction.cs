using TechFood.Application.Interfaces.DataSource;

namespace TechFood.Infra.Data.UoW;

public class UnitOfWorkTransaction(IEnumerable<IUnitOfWorkDataSource> uows) : IUnitOfWorkTransactionDataSource
{
    private readonly IEnumerable<IUnitOfWorkDataSource> _uows = uows;

    public async Task<bool> CommitAsync()
    {
        foreach (var uow in _uows)
        {
            await uow.CommitAsync();
        }

        return await Task.FromResult(true);
    }

    public async Task RollbackAsync()
    {
        foreach (var uow in _uows)
        {
            await uow.RollbackAsync();
        }
    }
}
