using Microsoft.EntityFrameworkCore;
using TechFood.Application.Interfaces.DataSource;
using TechFood.Common.DTO;
using TechFood.Infra.Data.Contexts;

namespace TechFood.Infra.Data.Repositories
{
    internal class UserRepository(TechFoodContext dbContext) : IUserDataSource
    {
        private readonly TechFoodContext _dbContext = dbContext;

        public async Task<Guid> CreateAsync(UserDTO user)
        {
            var entry = await _dbContext.AddAsync(user);

            return entry.Entity.Id;
        }

        public async Task<UserDTO?> GetByUsernameOrEmailAsync(string username)
        {
            return await _dbContext
                .Users
                .FirstOrDefaultAsync(
                    u => u.Username == username || u.Email != null && u.Email.Address! == username);
        }
    }
}
