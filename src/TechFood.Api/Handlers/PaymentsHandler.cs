using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TechFood.Application.Controllers;
using TechFood.Application.Interfaces.Controller;
using TechFood.Application.Interfaces.DataSource;
using TechFood.Application.Interfaces.Service;
using TechFood.Common.DTO.Payment;
using TechFood.Infra.Payments.MercadoPago;

namespace TechFood.Api.Handlers;

[ApiController()]
[Route("v1/payments")]
[Tags("Payments")]
public class PaymentsHandler : ControllerBase
{
    private readonly IPaymentController _paymentController;

    public PaymentsHandler(IUnitOfWorkDataSource unitOfWork,
                             IOrderDataSource orderDataSource,
                             IPreparationDataSource preparationDataSource,
                             IPaymentDataSource paymentDataSource,
                             IProductDataSource productDataSource,
                             IImageDataSource imageDataSource,
                             IOptions<MercadoPagoOptions> options,
                             IHttpClientFactory httpClientFactory,
                             IHttpContextAccessor httpContextAccessor,
                             IOrderNumberService orderNumberService)
    {
        var mercadoPagoPaymentService = new MercadoPagoPaymentService(options, httpClientFactory, httpContextAccessor);
        _paymentController = new PaymentController(unitOfWork,
                                                   orderDataSource,
                                                   preparationDataSource,
                                                   paymentDataSource,
                                                   productDataSource,
                                                   imageDataSource,
                                                   mercadoPagoPaymentService,
                                                   orderNumberService);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await _paymentController.GetByIdAsync(id);

        return result != null ? Ok(result) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreatePaymentRequestDTO paymentRequestDTO)
    {

        var result = await _paymentController.CreateAsync(paymentRequestDTO);

        return result != null ? Ok(result) : NotFound();
    }

    [HttpPatch("{id:Guid}")]
    public async Task<IActionResult> PayAsync(Guid id)
    {
        await _paymentController.ConfirmAsync(id);

        return Ok();
    }
}
