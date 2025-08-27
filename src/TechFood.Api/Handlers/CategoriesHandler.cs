using Microsoft.AspNetCore.Mvc;
using TechFood.Application.Controllers;
using TechFood.Application.Interfaces.Controller;
using TechFood.Application.Interfaces.DataSource;
using TechFood.Application.Interfaces.Presenter;
using TechFood.Common.DTO.Category;

namespace TechFood.Api.Handlers
{
    [ApiController()]
    [Route("v1/categories")]
    [Tags("Categories")]
    public class CategoriesHandler : ControllerBase
    {
        private readonly ICategoryController _categoryController;

        public CategoriesHandler(ICategoryDataSource _categoryDataSource,
                                 IImageUrlResolver imageUrlResolver,
                                 IUnitOfWorkDataSource unitOfWorkDataSource,
                                 IImageDataSource imageDataSource)
        {
            _categoryController = new CategoryController(_categoryDataSource,
                                                         imageUrlResolver,
                                                         imageDataSource,
                                                         unitOfWorkDataSource);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _categoryController.ListAllAsync();

            return Ok(result);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _categoryController.GetByIdAsync(id);

            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(CreateCategoryRequestDTO category)
        {
            var result = await _categoryController.AddAsync(category);

            return Ok(result);
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateCategoryRequestDTO category)
        {

            var result = await _categoryController.UpdateAsync(id, category);

            return result != null ? Ok(result) : NotFound();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _categoryController.DeleteAsync(id);

            return result ? NoContent() : NotFound();
        }
    }
}
