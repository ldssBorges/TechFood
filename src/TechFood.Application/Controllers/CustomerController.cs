using TechFood.Application.Gateway;
using TechFood.Application.Interfaces.Controller;
using TechFood.Application.Interfaces.DataSource;
using TechFood.Application.Presenters;
using TechFood.Common.DTO.Customer;
using TechFood.Domain.Interfaces.UseCase;
using TechFood.Domain.UseCases;

namespace TechFood.Application.Controllers
{
    public class CustomerController : ICustomerController
    {
        private readonly ICustomerUseCase _customerUseCase;
        public CustomerController(ICustomerDataSource customerDataSource,
                                  IUnitOfWorkDataSource unitOfWorkDataSource)
        {
            var customerGateway = new CustomerGateway(customerDataSource, unitOfWorkDataSource);
            _customerUseCase = new CustomerUseCase(customerGateway);
        }
        public async Task<CustomerPresenter?> CreateCustomerAsync(CreateCustomerRequestDTO customerDTO)
        {
            var customer = await _customerUseCase.CreateCustomerAsync(customerDTO);

            return customer is not null ?
                   CustomerPresenter.Create(customer) :
                   null;
        }

        public async Task<CustomerPresenter?> GetByDocumentAsync(string documentValue)
        {
            var customer = await _customerUseCase.GetByDocumentAsync(documentValue);

            return customer is not null ?
                   CustomerPresenter.Create(customer) :
                   null;
        }
    }
}
