# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY MediCore.sln ./
COPY src/MediCore.Domain/MediCore.Domain.csproj ./src/MediCore.Domain/
COPY src/MediCore.Application/MediCore.Application.csproj ./src/MediCore.Application/
COPY src/MediCore.Infrastructure/MediCore.Infrastructure.csproj ./src/MediCore.Infrastructure/
COPY src/MediCore.API/MediCore.API.csproj ./src/MediCore.API/

# Restore dependencies
RUN dotnet restore

# Copy everything else
COPY . .

# Build and publish
WORKDIR /src/src/MediCore.API
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Expose port
EXPOSE 8080

# Set environment variables
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Start the application
ENTRYPOINT ["dotnet", "MediCore.API.dll"]
