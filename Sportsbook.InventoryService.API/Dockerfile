FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
ARG GITHUB_TOKEN
ARG GITHUB_USER="github-actions"
WORKDIR /src
COPY ["Sportsbook.InventoryService.API/Sportsbook.InventoryService.API.csproj", "Sportsbook.InventoryService.API/"]

RUN if [ -z "$GITHUB_TOKEN" ]  ; then \
        echo "Argument not provided"; \
    else \
        dotnet nuget add source "https://nuget.pkg.github.com/ft-unified-platform/index.json" --name "GitHub" --username $GITHUB_USER --password $GITHUB_TOKEN --store-password-in-clear-text; \
    fi
RUN dotnet restore "Sportsbook.InventoryService.API/Sportsbook.InventoryService.API.csproj"
COPY . .
WORKDIR "/src/Sportsbook.InventoryService.API"
RUN dotnet build "./Sportsbook.InventoryService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sportsbook.InventoryService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sportsbook.InventoryService.API.dll"]