# Use the ASP.NET base image for the final runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use the .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files
COPY ["AssociationsClean.API/AssociationsClean.API.csproj", "AssociationsClean.API/"]
COPY ["AssociationsClean.Application/AssociationsClean.Application.csproj", "AssociationsClean.Application/"]
COPY ["AssociationsClean.Infrastructure/AssociationsClean.Infrastructure.csproj", "AssociationsClean.Infrastructure/"]
COPY ["AssociationsClean.Domain/AssociationsClean.Domain.csproj", "AssociationsClean.Domain/"]

# Restore dependencies
RUN dotnet restore "AssociationsClean.API/AssociationsClean.API.csproj"

# Copy all remaining source code
COPY . .

# Build the application
WORKDIR "/src/AssociationsClean.API"
RUN dotnet build "AssociationsClean.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the app
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AssociationsClean.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AssociationsClean.API.dll"]
