using TechFood.Domain.Entities;
using TechFood.Domain.Enums;

namespace TechFood.Domain.Interfaces.Gateway
{
    public interface ICustomerGateway
    {
        Task<Guid> CreateAsync(Customer customer);

        Task<Customer?> GetByDocumentAsync(DocumentType documentType, string documentValue);
    }
}
