# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/TechFood.Api/TechFood.Api.csproj", "src/TechFood.Api/"]
COPY ["src/TechFood.Application/TechFood.Application.csproj", "src/TechFood.Application/"]
COPY ["src/TechFood.Domain/TechFood.Domain.csproj", "src/TechFood.Domain/"]
COPY ["src/TechFood.Common/TechFood.Common.csproj", "src/TechFood.Common/"]
COPY ["src/TechFood.Infra.Data/TechFood.Infra.Data.csproj", "src/TechFood.Infra.Data/"]
COPY ["src/TechFood.Infra.ImageStore.LocalDisk/TechFood.Infra.ImageStore.LocalDisk.csproj", "src/TechFood.Infra.ImageStore.LocalDisk/"]
COPY ["src/TechFood.Infra.Payments.MercadoPago/TechFood.Infra.Payments.MercadoPago.csproj", "src/TechFood.Infra.Payments.MercadoPago/"]
RUN dotnet restore "./src/TechFood.Api/TechFood.Api.csproj"
COPY . .
WORKDIR "/src/src/TechFood.Api"
RUN dotnet build "./TechFood.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TechFood.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TechFood.Api.dll"]