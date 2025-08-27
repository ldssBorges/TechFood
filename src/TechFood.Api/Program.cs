using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using TechFood.Api.Filters;
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
        options.JsonSerializerOptions.Converters.Add(new JsonTimeSpanConverter());
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

    builder.Services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
            };
        });

    //builder.Services.AddApplication();
    builder.Services.AddInfraData();
    builder.Services.AddInfraMercadoPagoPayment();
    builder.Services.AddInfraImageStore();
}

var app = builder.Build();

//Run migrations
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<TechFoodContext>();
    dataContext.Database.Migrate();
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

app.UseStaticFiles(new StaticFileOptions
{
    RequestPath = app.Configuration["TechFoodStaticImagesUrl"],
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "images")),
});

app.UseRouting();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
