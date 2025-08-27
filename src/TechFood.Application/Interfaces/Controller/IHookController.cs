using TechFood.Common.DTO.Hook;

namespace TechFood.Application.Interfaces.Controller
{
    public interface IHookController
    {
        Task InvokeAsync(HookRequestDTO request);
    }
}
