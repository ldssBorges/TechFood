FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/TechFood.WebHook.Api/TechFood.WebHook.Api.csproj", "src/TechFood.WebHook.Api/"]
COPY ["src/TechFood.Application/TechFood.Application.csproj", "src/TechFood.Application/"]
COPY ["src/TechFood.Domain/TechFood.Domain.csproj", "src/TechFood.Domain/"]
COPY ["src/TechFood.Common/TechFood.Common.csproj", "src/TechFood.Common/"]
COPY ["src/TechFood.Infra.Data/TechFood.Infra.Data.csproj", "src/TechFood.Infra.Data/"]
COPY ["src/TechFood.Infra.ImageStore.LocalDisk/TechFood.Infra.ImageStore.LocalDisk.csproj", "src/TechFood.Infra.ImageStore.LocalDisk/"]
COPY ["src/TechFood.Infra.Payments.MercadoPago/TechFood.Infra.Payments.MercadoPago.csproj", "src/TechFood.Infra.Payments.MercadoPago/"]
RUN dotnet restore "src/TechFood.WebHook.Api/TechFood.WebHook.Api.csproj"
COPY . .
WORKDIR "/src/src/TechFood.WebHook.Api"
RUN dotnet build "TechFood.WebHook.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TechFood.WebHook.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TechFood.WebHook.Api.dll"]
