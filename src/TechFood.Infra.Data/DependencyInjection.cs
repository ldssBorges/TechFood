using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TechFood.Application.Interfaces.DataSource;
using TechFood.Application.Interfaces.Presenter;
using TechFood.Application.Interfaces.Service;
using TechFood.Infra.Data.Contexts;
using TechFood.Infra.Data.Repositories;
using TechFood.Infra.Data.Services;
using TechFood.Infra.Data.UoW;

namespace TechFood.Infra.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraData(this IServiceCollection services)
    {
        //Context
        services.AddScoped<TechFoodContext>();
        services.AddDbContext<TechFoodContext>((serviceProvider, options) =>
        {
            var config = serviceProvider.GetRequiredService<IConfiguration>();

            options.LogTo(Console.WriteLine, LogLevel.Information);

            // This is optional but useful for seeing parameter values in your SQL.
            // WARNING: DO NOT USE IN PRODUCTION, as it may log sensitive data.
            options.EnableSensitiveDataLogging();

            options.UseSqlServer(config.GetConnectionString("DataBaseConection"));
        });

        //UoW
        services.AddScoped<IUnitOfWorkTransactionDataSource, UnitOfWorkTransaction>();
        services.AddScoped<IUnitOfWorkDataSource, UnitOfWork>();
        services.AddScoped<IUnitOfWorkDataSource, AnotherUnitOfWork>();

        //Data
        services.AddScoped<ICategoryDataSource, CategoryRepository>();
        services.AddTransient<IImageUrlResolver, ImageUrlResolver>();
        services.AddScoped<IProductDataSource, ProductRepository>();
        services.AddScoped<IOrderDataSource, OrderRepository>();
        services.AddScoped<ICustomerDataSource, CustomerRepository>();
        services.AddScoped<IPaymentDataSource, PaymentRepository>();
        services.AddSingleton<IOrderNumberService, OrderNumberService>();
        //services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPreparationDataSource, PreparationRepository>();
        //services.AddScoped<IReadOnlyQuery<GetPreparationMonitorResult>, OrderMonitorQuery>();

        return services;
    }
}
