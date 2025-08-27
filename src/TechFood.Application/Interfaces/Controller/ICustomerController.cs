using TechFood.Application.Presenters;
using TechFood.Common.DTO.Customer;

namespace TechFood.Application.Interfaces.Controller
{
    public interface ICustomerController
    {
        Task<CustomerPresenter?> CreateCustomerAsync(CreateCustomerRequestDTO data);

        Task<CustomerPresenter?> GetByDocumentAsync(string documentValue);
    }
}
