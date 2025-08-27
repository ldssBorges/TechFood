using TechFood.Application.Gateway;
using TechFood.Application.Interfaces.Controller;
using TechFood.Application.Interfaces.DataSource;
using TechFood.Application.Interfaces.Presenter;
using TechFood.Application.Presenters;
using TechFood.Common.DTO.Product;
using TechFood.Domain.Interfaces.UseCase;
using TechFood.Domain.UseCases;

namespace TechFood.Application.Controllers
{
    public class ProductController : IProductController
    {
        private readonly IProductUseCase _productUseCase;
        private readonly IImageUrlResolver _imageUrlResolver;
        public ProductController(IProductDataSource productDataSource,
                                  IImageUrlResolver imageUrlResolver,
                                  IImageDataSource imageDataSource,
                                  IUnitOfWorkDataSource unitOfWorkDataSource,
                                  ICategoryDataSource categoryDataSource)
        {
            var productGateway = new ProductGateway(productDataSource, imageDataSource, unitOfWorkDataSource);
            var categoryGateway = new CategoryGateway(categoryDataSource, imageDataSource, unitOfWorkDataSource);
            _productUseCase = new ProductUseCase(productGateway, categoryGateway);
            _imageUrlResolver = imageUrlResolver;
        }

        public async Task<ProductPresenter?> AddAsync(CreateProductRequestDTO product)
        {
            var imageFileName = _imageUrlResolver.CreateImageFileName(product.Name, product.File.ContentType);

            var result = await _productUseCase.AddAsync(product, imageFileName);

            return result is not null ?
                   ProductPresenter.Create(result, _imageUrlResolver) :
                   null;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _productUseCase.DeleteAsync(id);
        }

        public async Task<ProductPresenter?> GetByIdAsync(Guid id)
        {
            var category = await _productUseCase.GetByIdAsync(id);

            return category is not null ?
                   ProductPresenter.Create(category, _imageUrlResolver) :
                   null;
        }

        public async Task<IEnumerable<ProductPresenter>> ListAllAsync()
        {
            var products = await _productUseCase.ListAllAsync();

            return products.Select(products => ProductPresenter.Create(products, _imageUrlResolver)).ToList();
        }

        public async Task<ProductPresenter?> UpdateAsync(Guid id, UpdateProductRequestDTO productDTO)
        {
            var fileName = string.Empty;

            if (productDTO.File != null)
            {
                fileName = _imageUrlResolver.CreateImageFileName(productDTO.Name, productDTO.File.ContentType);
            }

            var product = await _productUseCase.UpdateAsync(id, productDTO, fileName);
            return product is not null ?
                   ProductPresenter.Create(product, _imageUrlResolver) :
                   null;
        }
    }
}
