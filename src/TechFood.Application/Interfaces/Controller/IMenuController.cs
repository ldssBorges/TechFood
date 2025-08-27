using TechFood.Application.Presenters;

namespace TechFood.Application.Interfaces.Controller
{
    public interface IMenuController
    {
          Task<MenuPresenter?> GetAsync();
    }
}
