using Microsoft.AspNetCore.Http;
using TechFood.Application.Interfaces.DataSource;
using TechFood.Common.DTO;
using TechFood.Domain.Entities;
using TechFood.Domain.Interfaces.Gateway;

namespace TechFood.Application.Gateway
{
    public class ProductGateway : IProductGateway
    {
        private readonly IProductDataSource _productDataSource;
        private readonly IImageDataSource _imageDataSource;
        private readonly IUnitOfWorkDataSource _unitOfWorkDataSource;

        public ProductGateway(IProductDataSource productDataSource,
                               IImageDataSource imageDataSource,
                               IUnitOfWorkDataSource unitOfWorkDataSource)
            : this(productDataSource, unitOfWorkDataSource)
        {
            _imageDataSource = imageDataSource;
        }

        public ProductGateway(IProductDataSource productDataSource,
                               IUnitOfWorkDataSource unitOfWorkDataSource)
        {
            _productDataSource = productDataSource;
            _unitOfWorkDataSource = unitOfWorkDataSource;
        }

        public async Task AddAsync(Product product)
        {
            var productDTO = new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                ImageFileName = product.ImageFileName,
                IsDeleted = product.IsDeleted,
                Description = product.Description,
                CategoryDTOId = product.CategoryId,
                OutOfStock = product.OutOfStock,
                Price = product.Price
            };

            await _productDataSource.AddAsync(productDTO);

            await _unitOfWorkDataSource.CommitAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            var productDTO = new ProductDTO
            {
                Id = product.Id,
                Description = product.Description,
                CategoryDTOId = product.CategoryId,
                ImageFileName = product.ImageFileName,
                Name = product.Name,
                OutOfStock = product.OutOfStock,
                Price = product.Price,
                IsDeleted = product.IsDeleted
            };

            await _productDataSource.DeleteAsync(productDTO);

            await _unitOfWorkDataSource.CommitAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            var productDTO = new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                ImageFileName = product.ImageFileName,
                IsDeleted = product.IsDeleted,
                Description = product.Description,
                CategoryDTOId = product.CategoryId,
                OutOfStock = product.OutOfStock,
                Price = product.Price
            };

            await _productDataSource.UpdateAsync(productDTO);

            await _unitOfWorkDataSource.CommitAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            var productDTO = await _productDataSource.GetByIdAsync(id);

            return productDTO is not null ?
                               new Product(productDTO.Id,
                                           productDTO.Name,
                                           productDTO.Description,
                                           productDTO.CategoryDTOId,
                                           productDTO.ImageFileName,
                                           productDTO.OutOfStock,
                                           productDTO.Price) : null;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var productDTO = await _productDataSource.GetAllAsync();

            return productDTO.Select(product =>
                        new Product(product.Id,
                                    product.Name,
                                    product.Description,
                                    product.CategoryDTOId,
                                    product.ImageFileName,
                                    product.OutOfStock,
                                    product.Price)).ToList();
        }

        public async Task SaveImageAsync(IFormFile file, string fileName)
        {
            await _imageDataSource.SaveAsync(file.OpenReadStream(), fileName, nameof(Product));
        }

        public async Task DeleteImageAsync(Product product)
        {
            await _imageDataSource.DeleteAsync(product.ImageFileName, nameof(Product));
        }
    }
}
