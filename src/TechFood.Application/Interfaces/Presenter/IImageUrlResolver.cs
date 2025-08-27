namespace TechFood.Application.Interfaces.Presenter
{
    public interface IImageUrlResolver
    {
        string BuildFilePath(string folderName, string imageFileName);

        string CreateImageFileName(string categoryName, string contentType);
    }
}
