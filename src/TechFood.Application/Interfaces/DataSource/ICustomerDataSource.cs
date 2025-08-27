using TechFood.Common.DTO;
using TechFood.Common.DTO.Enums;

namespace TechFood.Application.Interfaces.DataSource
{
    public interface ICustomerDataSource
    {
        Task<Guid> CreateAsync(CustomerDTO customer);

        Task<CustomerDTO?> GetByDocumentAsync(DocumentTypeDTO documentType, string documentValue);
    }
}
