using TechFood.Application.Controllers;
using TechFood.Application.Presenters;

namespace TechFood.Application.Interfaces.Controller;

public interface IPreparationController
{
    Task<IEnumerable<PreparationMonitorPresenter>> GetAllPreparationOrdersAsync();
    Task<PreparationPresenter> GetPreparationByOrderIdAsync(Guid orderId);
    Task<IEnumerable<PreparationPresenter>> GetAllAsync();
    Task StartAsync(Guid id);
    Task FinishAsync(Guid id);
    Task CancelAsync(Guid id);
}
