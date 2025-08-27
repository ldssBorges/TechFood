using TechFood.Common.DTO;

namespace TechFood.Application.Interfaces.DataSource
{
    public interface IUserDataSource
    {
        Task<Guid> CreateAsync(UserDTO user);

        Task<UserDTO?> GetByUsernameOrEmailAsync(string username);
    }
}
