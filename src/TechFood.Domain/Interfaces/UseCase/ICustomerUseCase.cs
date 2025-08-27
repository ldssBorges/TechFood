using TechFood.Common.DTO.Customer;
using TechFood.Domain.Entities;

namespace TechFood.Domain.Interfaces.UseCase
{
    public interface ICustomerUseCase
    {
        Task<Customer?> CreateCustomerAsync(CreateCustomerRequestDTO data);

        Task<Customer?> GetByDocumentAsync(string documentType);
    }
}
