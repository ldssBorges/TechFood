using TechFood.Application.Interfaces.Presenter;



namespace TechFood.Application.Presenters
{
    public class MenuPresenter
    {
        public static MenuPresenter Create(IEnumerable<Domain.Entities.Product> products, IEnumerable<Domain.Entities.Category> categories,
            IImageUrlResolver _imageUrlResolver)
        {
            var menu = new MenuPresenter
            {
                Categories = categories.Select(c => new MenuPresenter.Category
                {
                    Id = c.Id,
                    Name = c.Name,
                    SortOrder = c.SortOrder,
                    ImageUrl = _imageUrlResolver.BuildFilePath(
                        nameof(Category).ToLower(),
                        c.ImageFileName),
                    Products = products.Where(p => p.CategoryId == c.Id).
                    Select(p => new MenuPresenter.ProductMenu
                    {
                        Id = p.Id,
                        Name = p.Name,
                        CategoryId = p.CategoryId,
                        Description = p.Description,
                        Price = p.Price,
                        ImageUrl = _imageUrlResolver.BuildFilePath(
                            nameof(Domain.Entities.Product).ToLower(),
                            p.ImageFileName)
                    }).ToList()
                }).ToList()
            };

            return menu;
        }
        public List<Category> Categories { get; set; } = [];

        public class Category
        {
            public Guid Id { get; set; }

            public string Name { get; set; } = null!;

            public string ImageUrl { get; set; } = null!;

            public int SortOrder { get; set; }

            public List<ProductMenu> Products { get; set; } = [];
        }

        public class ProductMenu
        {
            public Guid Id { get; set; }

            public Guid CategoryId { get; set; }

            public string Name { get; set; } = null!;

            public string Description { get; set; } = null!;

            public decimal Price { get; set; }

            public string ImageUrl { get; set; } = null!;
        }
    }
}
