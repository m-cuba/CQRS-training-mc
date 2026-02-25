FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore Sportsbook.InventoryService.Audit.API/Sportsbook.InventoryService.Audit.API.csproj
RUN dotnet publish Sportsbook.InventoryService.Audit.API/Sportsbook.InventoryService.Audit.API.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Sportsbook.InventoryService.Audit.API.dll"]