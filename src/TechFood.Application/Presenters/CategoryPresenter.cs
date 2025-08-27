using TechFood.Application.Interfaces.Presenter;
using TechFood.Domain.Entities;

namespace TechFood.Application.Presenters
{
    public class CategoryPresenter
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public static CategoryPresenter Create(Category category, IImageUrlResolver imageUrlResolver)
        {
            return new CategoryPresenter
            {
                Id = category.Id,
                Name = category.Name,
                ImageUrl = imageUrlResolver.BuildFilePath(nameof(Category).ToLower(), category.ImageFileName)
            };
        }

    }
}
