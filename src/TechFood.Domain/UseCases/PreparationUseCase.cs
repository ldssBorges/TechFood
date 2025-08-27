using TechFood.Domain.Entities;
using TechFood.Domain.Enums;
using TechFood.Domain.Interfaces.Gateway;
using TechFood.Domain.Interfaces.UseCase;

namespace TechFood.Domain.UseCases;

public class PreparationUseCase : IPreparationUseCase
{
    private readonly IPreparationGateway _preparationGateway;
        
    public PreparationUseCase(
        IPreparationGateway preparationGateway)
    {
        _preparationGateway = preparationGateway;
    }

    public Task<Preparation> GetPreparationByOrderIdAsync(Guid orderId)
    {
        return _preparationGateway.GetByOrderIdAsync(orderId);
    }

    public Task<IEnumerable<Preparation>> GetAllAsync()
    {
        return _preparationGateway.GetAllAsync();
    }

    public async Task<Preparation> GetByIdAsync(Guid id)
    {
        var preparation = await _preparationGateway.GetByIdAsync(id);
        if (preparation == null)
        {
            throw new KeyNotFoundException($"Preparation with ID {id} not found.");
        }

        return preparation;
    }

    public async Task StartAsync(Guid id)
    {
        var preparation = await _preparationGateway.GetByIdAsync(id);
        if (preparation == null)
        {
            throw new ApplicationException($"Preparation with ID {id} not found.");
        }
        preparation.Start();
        await _preparationGateway.UpdateAsync(preparation);
    }

    public async Task FinishAsync(Guid id)
    {
        var preparation = await _preparationGateway.GetByIdAsync(id);
        if (preparation == null)
        {
            throw new ApplicationException($"Preparation with ID {id} not found.");
        }
        preparation.Finish();
        await _preparationGateway.UpdateAsync(preparation);
    }

    public async Task CancelAsync(Guid id)
    {
        var preparation = await _preparationGateway.GetByIdAsync(id);
        if (preparation == null)
        {
            throw new ApplicationException($"Preparation with ID {id} not found.");
        }
        preparation.Cancel();
        await _preparationGateway.UpdateAsync(preparation);
    }
}
