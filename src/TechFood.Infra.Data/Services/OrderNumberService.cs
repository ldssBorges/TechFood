using TechFood.Application.Interfaces.Service;

namespace TechFood.Infra.Data.Services
{
    public class OrderNumberService : IOrderNumberService
    {
        private int _count;
        private DateTime _lastResetTime = DateTime.UtcNow.Date;

        private readonly SemaphoreSlim _semaphore = new(1, 1);

        public async Task<int> GetAsync()
        {
            await _semaphore.WaitAsync();

            try
            {
                if (_lastResetTime.Date != DateTime.UtcNow.Date)
                {
                    _count = 0;
                    _lastResetTime = DateTime.UtcNow.Date;
                }

                _count++;
                return _count;
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
