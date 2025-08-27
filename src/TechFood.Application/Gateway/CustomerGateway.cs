using TechFood.Application.Interfaces.DataSource;
using TechFood.Common.DTO;
using TechFood.Common.DTO.Enums;
using TechFood.Common.DTO.ValueObjects;
using TechFood.Domain.Entities;
using TechFood.Domain.Enums;
using TechFood.Domain.Interfaces.Gateway;
using TechFood.Domain.ValueObjects;

namespace TechFood.Application.Gateway
{
    public class CustomerGateway : ICustomerGateway
    {
        private readonly ICustomerDataSource _customerDataSource;
        private readonly IUnitOfWorkDataSource _unitOfWorkDataSource;
        public CustomerGateway(ICustomerDataSource customerDataSource,
                               IUnitOfWorkDataSource unitOfWorkDataSource)
        {
            _customerDataSource = customerDataSource;
            _unitOfWorkDataSource = unitOfWorkDataSource;
        }

        public async Task<Guid> CreateAsync(Customer customer)
        {
            var customerDto = new CustomerDTO
            {
                Id = customer.Id,
                Name = new NameDTO() { FullName = customer.Name.FullName },
                Document = new DocumentDTO()
                {
                    Type = (DocumentTypeDTO)customer.Document.Type,
                    Value = customer.Document.Value
                },
                Email = new EmailDTO()
                {
                    Address = customer.Email?.Address ?? string.Empty
                },
                Phone = new PhoneDTO()
                {
                    CountryCode = customer.Phone?.CountryCode ?? string.Empty,
                    DDD = customer.Phone?.DDD ?? string.Empty,
                    Number = customer.Phone?.Number ?? string.Empty
                }
            };

            return await _customerDataSource.CreateAsync(customerDto);
        }

        public async Task<Customer?> GetByDocumentAsync(DocumentType documentType, string documentValue)
        {
            var customerDTO = await _customerDataSource.GetByDocumentAsync((DocumentTypeDTO)documentType, documentValue);

            return customerDTO is not null ?
                                   new Customer(
                                       new Name(customerDTO.Name.FullName),
                                       new Email(customerDTO.Email.Address),
                                       new Document((DocumentType)customerDTO.Document.Type,
                                                            customerDTO.Document.Value),
                                       customerDTO.Phone is not null
                                           ? new Phone(customerDTO.Phone.CountryCode,
                                                       customerDTO.Phone.DDD,
                                                       customerDTO.Phone.Number)
                                           : null,
                                       customerDTO.Id
                                       ) :
                                   null;
        }
    }
}
