namespace TechFood.Application.Interfaces.DataSource
{
    public interface IUnitOfWorkDataSource
    {
        Task<bool> CommitAsync();

        Task RollbackAsync();
    }
}
