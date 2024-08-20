#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Otus.Teaching.Pcf.GivingToCustomer/Otus.Teaching.Pcf.GivingToCustomer.IntegrationHostedService/Otus.Teaching.Pcf.GivingToCustomer.IntegrationHostedService.csproj", "Otus.Teaching.Pcf.GivingToCustomer/Otus.Teaching.Pcf.GivingToCustomer.IntegrationHostedService/"]
RUN dotnet restore "./Otus.Teaching.Pcf.GivingToCustomer/Otus.Teaching.Pcf.GivingToCustomer.IntegrationHostedService/Otus.Teaching.Pcf.GivingToCustomer.IntegrationHostedService.csproj"
COPY . .
WORKDIR "/src/Otus.Teaching.Pcf.GivingToCustomer/Otus.Teaching.Pcf.GivingToCustomer.IntegrationHostedService"
RUN dotnet build "./Otus.Teaching.Pcf.GivingToCustomer.IntegrationHostedService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Otus.Teaching.Pcf.GivingToCustomer.IntegrationHostedService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Otus.Teaching.Pcf.GivingToCustomer.IntegrationHostedService.dll"]