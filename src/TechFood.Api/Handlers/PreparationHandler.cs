using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TechFood.Application.Controllers;
using TechFood.Application.Interfaces.Controller;
using TechFood.Application.Interfaces.DataSource;

namespace TechFood.Api.Handlers;

[ApiController()]
[Route("v1/preparation")]
[Tags("Preparation")]

public class PreparationHandler : ControllerBase
{
    private readonly IPreparationController _preparationController;
    
    public PreparationHandler(
        IPreparationDataSource preparationDataSource,
         IOrderDataSource orderDataSource,
                             IProductDataSource productDataSource,
                             IImageDataSource imageDataSource,
                             ICategoryDataSource categoryDataSource,
        IUnitOfWorkDataSource unitOfWorkDataSource
        )
    {
        _preparationController = new PreparationController(
            preparationDataSource,
            orderDataSource,
            productDataSource,
            categoryDataSource,
            imageDataSource,
               unitOfWorkDataSource);


                            
    }

    [HttpGet]
    [Route("orders")]
    public async Task<IActionResult> GetAllPreparationOrdersAsync()
    {
        return Ok(await _preparationController.GetAllPreparationOrdersAsync());
    }

    [HttpGet]
    [Route("{orderId:guid}/number")]
    public async Task<IActionResult> GetPreparationByOrderIdAsync(Guid orderId)
    {
        var preparationPresenter = await _preparationController.GetPreparationByOrderIdAsync(orderId);

        if (preparationPresenter is null)
            return NotFound("Preparation not found for the given order ID.");

        return Ok(preparationPresenter);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _preparationController.GetAllAsync();

        return Ok(result);
    }

    [HttpPatch]
    [Route("{id:guid}/start")]
    public async Task<IActionResult> PrepareAsync(Guid id)
    {
        await _preparationController.StartAsync(id);

        return Ok("Preparation started successfully.");
    }

    [HttpPatch]
    [Route("{id:guid}/finish")]
    public async Task<IActionResult> FinishAsync(Guid id)
    {
        await _preparationController.FinishAsync(id);

        return Ok("Preparation finished successfully.");
    }

    [HttpPatch]
    [Route("{id:guid}/cancel")]
    public async Task<IActionResult> CancelAsync(Guid id)
    {
        await _preparationController.CancelAsync(id);

        return Ok("Preparation canceled successfully");
    }
}

