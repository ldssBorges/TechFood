using Microsoft.AspNetCore.Http;
using TechFood.Application.Interfaces.DataSource;
using TechFood.Common.DTO;
using TechFood.Domain.Entities;
using TechFood.Domain.Interfaces.Gateway;

namespace TechFood.Application.Gateway
{
    public class CategoryGateway : ICategoryGateway
    {

        private readonly ICategoryDataSource _categoryDataSource;
        private readonly IImageDataSource _imageDataSource;
        private readonly IUnitOfWorkDataSource _unitOfWorkDataSource;
        public CategoryGateway(ICategoryDataSource categoryDataSource,
                               IImageDataSource imageDataSource,
                               IUnitOfWorkDataSource unitOfWorkDataSource)
        {
            _categoryDataSource = categoryDataSource;
            _imageDataSource = imageDataSource;
            _unitOfWorkDataSource = unitOfWorkDataSource;
        }

        public async Task AddAsync(Category entity)
        {
            var categoryDTO = new CategoryDTO
            {
                Name = entity.Name,
                ImageFileName = entity.ImageFileName,
                SortOrder = entity.SortOrder,
                IsDeleted = entity.IsDeleted
            };

            await _categoryDataSource.AddAsync(categoryDTO);

            await _unitOfWorkDataSource.CommitAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            var categoryDTO = new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                ImageFileName = category.ImageFileName,
                SortOrder = category.SortOrder,
                IsDeleted = category.IsDeleted
            };

            await _categoryDataSource.DeleteAsync(categoryDTO);

            await _unitOfWorkDataSource.CommitAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            var categoryDTO = new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                ImageFileName = category.ImageFileName,
                SortOrder = category.SortOrder,
                IsDeleted = category.IsDeleted
            };

            await _categoryDataSource.UpdateAsync(categoryDTO);

            await _unitOfWorkDataSource.CommitAsync();
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            var categoryDTO = await _categoryDataSource.GetByIdAsync(id);

            return categoryDTO is not null ?
                               new Category(categoryDTO.Name,
                                            categoryDTO.ImageFileName,
                                            categoryDTO.SortOrder,
                                            categoryDTO.IsDeleted,
                                            categoryDTO.Id) :
                               null;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            var categoryDTO = await _categoryDataSource.GetAllAsync();

            return categoryDTO.Select(x =>
                        new Category(x.Name,
                                     x.ImageFileName,
                                     x.SortOrder,
                                     x.IsDeleted,
                                     x.Id)
                        ).ToList();
        }

        public async Task SaveImageAsync(IFormFile file, string fileName)
        {
            await _imageDataSource.SaveAsync(file.OpenReadStream(), fileName, nameof(Category));
        }

        public async Task DeleteImageAsync(Category category)
        {
            await _imageDataSource.DeleteAsync(category.ImageFileName, nameof(Category));
        }
    }
}
