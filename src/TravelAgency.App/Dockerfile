#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/TravelAgency.App/TravelAgency.App.csproj", "src/TravelAgency.App/"]
COPY ["src/TravelAgency.Persistence/TravelAgency.Persistence.csproj", "src/TravelAgency.Persistence/"]
COPY ["src/TravelAgency.Infrastructure/TravelAgency.Infrastructure.csproj", "src/TravelAgency.Infrastructure/"]
COPY ["src/TravelAgency.Application/TravelAgency.Application.csproj", "src/TravelAgency.Application/"]
COPY ["src/TravelAgency.Domain/TravelAgency.Domain.csproj", "src/TravelAgency.Domain/"]
COPY ["src/TravelAgency.Presentation/TravelAgency.Presentation.csproj", "src/TravelAgency.Presentation/"]
RUN dotnet restore "./src/TravelAgency.App/TravelAgency.App.csproj"
COPY . .
WORKDIR "/src/src/TravelAgency.App"
RUN dotnet build "./TravelAgency.App.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TravelAgency.App.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TravelAgency.App.dll"]