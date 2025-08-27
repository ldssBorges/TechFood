using Microsoft.EntityFrameworkCore;
using TechFood.Application.Interfaces.DataSource;
using TechFood.Common.DTO;
using TechFood.Common.DTO.Enums;
using TechFood.Infra.Data.Contexts;

namespace TechFood.Infra.Data.Repositories
{
    internal class CustomerRepository(TechFoodContext dbContext) : ICustomerDataSource
    {
        private readonly TechFoodContext _dbContext = dbContext;

        public async Task<Guid> CreateAsync(CustomerDTO customer)
        {
            var entry = await _dbContext.AddAsync(customer);

            await entry.Context.SaveChangesAsync();

            return entry.Entity.Id;
        }

        public async Task<CustomerDTO?> GetByDocumentAsync(DocumentTypeDTO documentType, string documentValue)
        {
            return await _dbContext.Customers
                .FirstOrDefaultAsync(c => c.Document.Type == documentType && c.Document.Value == documentValue);
        }
    }
}
