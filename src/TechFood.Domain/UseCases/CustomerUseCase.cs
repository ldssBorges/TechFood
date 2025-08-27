using TechFood.Common.DTO.Customer;
using TechFood.Domain.Entities;
using TechFood.Domain.Enums;
using TechFood.Domain.Interfaces.Gateway;
using TechFood.Domain.Interfaces.UseCase;
using TechFood.Domain.ValueObjects;

namespace TechFood.Domain.UseCases
{
    public class CustomerUseCase : ICustomerUseCase
    {
        private readonly ICustomerGateway _customerGateway;
        public CustomerUseCase(ICustomerGateway customerGateway)
        {
            _customerGateway = customerGateway;
        }

        public async Task<Customer?> CreateCustomerAsync(CreateCustomerRequestDTO customerRequestDTO)
        {
            var document = new Document(DocumentType.CPF, customerRequestDTO.CPF);

            var customerFound = await _customerGateway.GetByDocumentAsync(document.Type, document.Value);

            if (customerFound != null)
            {
                throw new ApplicationException("Já existe um cliente com esse CPF.");
            }

            var customer = new Customer(
                new Name(customerRequestDTO.Name),
                new Email(customerRequestDTO.Email),
                document,
                null
            );

            await _customerGateway.CreateAsync(customer);

            return customer;
        }

        public async Task<Customer?> GetByDocumentAsync(string documentValue)
        {
            var document = new Document(DocumentType.CPF, documentValue);

            var customer = await _customerGateway.GetByDocumentAsync(document.Type, document.Value);

            if (customer == null)
            {
                return null;
            }

            return customer;
        }
    }
}
