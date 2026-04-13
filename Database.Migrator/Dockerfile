FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Database.Migrator/Database.Migrator.csproj", "Database.Migrator/"]
COPY ["Database/Database.csproj", "Database/"]

RUN dotnet restore "Database.Migrator/Database.Migrator.csproj"

COPY . .
WORKDIR /src/Database.Migrator
RUN dotnet publish "Database.Migrator.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Database.Migrator.dll"]