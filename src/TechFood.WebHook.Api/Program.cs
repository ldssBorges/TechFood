using Microsoft.AspNetCore.HttpOverrides;
using System.Text.Json.Serialization;
using TechFood.WebHook.Api.Filters;
using TechFood.Infra.Data;
using TechFood.Infra.Data.Contexts;
using TechFood.Infra.Data.NamingPolicy;
using TechFood.Infra.ImageStore.LocalDisk.Configuration;
using TechFood.Infra.Payments.MercadoPago;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddControllers(options =>
        {
            options.Filters.Add<ExceptionFilter>();
            options.Filters.Add<ModelStateFilter>();
        })

    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    })

    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(new UpperCaseNamingPolicy()));
    });

    builder.Services.AddCors();

    builder.Services.Configure<ForwardedHeadersOptions>(options =>
    {
        options.ForwardedHeaders = ForwardedHeaders.All;
        options.KnownNetworks.Clear();
        options.KnownProxies.Clear();
    });

    builder.Services.AddHttpContextAccessor();

    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "TechFood API V1",
            Version = "v1",
            Description = "TechFood API V1",
        });
    });

    builder.Services.AddHealthChecks();

    builder.Services.AddInfraData();
    builder.Services.AddInfraMercadoPagoPayment();
    builder.Services.AddInfraImageStore();
}

var app = builder.Build();

//Run migrations
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<TechFoodContext>();
}

app.UseForwardedHeaders();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
    app.UseHttpsRedirection();
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger(options =>
    {
        options.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0;
    });
    app.UseSwaggerUI();
}

app.UseHealthChecks("/health");

app.UseRouting();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
