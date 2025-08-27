using Microsoft.Extensions.DependencyInjection;
using TechFood.Application.Interfaces.DataSource;

namespace TechFood.Infra.ImageStore.LocalDisk.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraImageStore(this IServiceCollection services)
        {
            services.AddScoped<IImageDataSource, LocalDiskImageStorageService>();

            return services;
        }
    }
}
