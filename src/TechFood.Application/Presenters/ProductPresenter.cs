using TechFood.Application.Interfaces.Presenter;
using TechFood.Domain.Entities;

namespace TechFood.Application.Presenters
{
    public class ProductPresenter
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid CategoryId { get; set; }

        public bool OutOfStock { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public static ProductPresenter Create(Product product, IImageUrlResolver imageUrlResolver)
        {
            return new ProductPresenter
            {
                Id = product.Id,
                Name = product.Name,
                ImageUrl = imageUrlResolver.BuildFilePath(nameof(Product).ToLower(), product.ImageFileName),
                Description = product.Description,
                CategoryId = product.CategoryId,
                OutOfStock = product.OutOfStock,
                Price = product.Price
            };
        }
    }
}
