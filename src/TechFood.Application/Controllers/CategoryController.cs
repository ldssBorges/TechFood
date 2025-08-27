using TechFood.Application.Gateway;
using TechFood.Application.Interfaces.Controller;
using TechFood.Application.Interfaces.DataSource;
using TechFood.Application.Interfaces.Presenter;
using TechFood.Application.Presenters;
using TechFood.Common.DTO.Category;
using TechFood.Domain.Interfaces.UseCase;
using TechFood.Domain.UseCases;

namespace TechFood.Application.Controllers
{
    public class CategoryController : ICategoryController
    {
        private readonly ICategoryUseCase _categoryUseCase;
        private readonly IImageUrlResolver _imageUrlResolver;
        public CategoryController(ICategoryDataSource categoryDataSource,
                                  IImageUrlResolver imageUrlResolver,
                                  IImageDataSource imageDataSource,
                                  IUnitOfWorkDataSource unitOfWorkDataSource)
        {
            var categoryGateway = new CategoryGateway(categoryDataSource, imageDataSource, unitOfWorkDataSource);
            _categoryUseCase = new CategoryUseCase(categoryGateway);
            _imageUrlResolver = imageUrlResolver;
        }

        public async Task<CategoryPresenter?> AddAsync(CreateCategoryRequestDTO category)
        {
            var imageFileName = _imageUrlResolver.CreateImageFileName(category.Name, category.File.ContentType);

            var result = await _categoryUseCase.AddAsync(category, imageFileName);

            return result is not null ?
                   CategoryPresenter.Create(result, _imageUrlResolver) :
                   null;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _categoryUseCase.DeleteAsync(id);
        }

        public async Task<CategoryPresenter?> GetByIdAsync(Guid id)
        {
            var category = await _categoryUseCase.GetByIdAsync(id);

            return category is not null ?
                   CategoryPresenter.Create(category, _imageUrlResolver) :
                   null;
        }

        public async Task<IEnumerable<CategoryPresenter>> ListAllAsync()
        {
            var categories = await _categoryUseCase.ListAllAsync();

            return categories.Select(category => CategoryPresenter.Create(category, _imageUrlResolver)).ToList();
        }

        public async Task<CategoryPresenter?> UpdateAsync(Guid id, UpdateCategoryRequestDTO categoryDTO)
        {
            var fileName = string.Empty;

            if (categoryDTO.File != null)
            {
                fileName = _imageUrlResolver.CreateImageFileName(categoryDTO.Name, categoryDTO.File.ContentType);
            }

            var categories = await _categoryUseCase.UpdateAsync(id, categoryDTO, fileName);
            return categories is not null ?
                   CategoryPresenter.Create(categories, _imageUrlResolver) :
                   null;
        }
    }
}
