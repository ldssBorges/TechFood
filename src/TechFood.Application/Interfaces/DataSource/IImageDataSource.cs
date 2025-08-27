namespace TechFood.Application.Interfaces.DataSource
{
    public interface IImageDataSource
    {
        Task SaveAsync(Stream imageStream, string fileName, string folder);

        Task DeleteAsync(string fileName, string folder);
    }
}
