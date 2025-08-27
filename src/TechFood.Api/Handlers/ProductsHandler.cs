using Microsoft.AspNetCore.Mvc;
using TechFood.Application.Controllers;
using TechFood.Application.Interfaces.Controller;
using TechFood.Application.Interfaces.DataSource;
using TechFood.Application.Interfaces.Presenter;
using TechFood.Common.DTO.Product;

namespace TechFood.Api.Handlers
{
    [ApiController()]
    [Route("v1/products")]
    [Tags("Products")]
    public class ProductsHandler : ControllerBase
    {
        private readonly IProductController _productController;

        public ProductsHandler(IProductDataSource productDataSource,
                               IImageUrlResolver imageUrlResolver,
                               IUnitOfWorkDataSource unitOfWorkDataSource,
                               IImageDataSource imageDataSource,
                               ICategoryDataSource categoryDataSource)
        {
            _productController = new ProductController(productDataSource,
                                                       imageUrlResolver,
                                                       imageDataSource,
                                                       unitOfWorkDataSource,
                                                       categoryDataSource);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _productController.ListAllAsync();

            return Ok(result);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _productController.GetByIdAsync(id);

            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(CreateProductRequestDTO product)
        {
            var result = await _productController.AddAsync(product);

            return Ok(result);
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateProductRequestDTO product)
        {

            var result = await _productController.UpdateAsync(id, product);

            return result != null ? Ok(result) : NotFound();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _productController.DeleteAsync(id);

            return result ? NoContent() : NotFound();
        }
    }
}
