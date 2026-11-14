# Dockerfile for doofplus-platform
# Builds the DoofPlus RESTful API with the .NET 10 SDK and runs it on the ASP.NET Core 10 runtime.
# Multi-stage build: the first stage publishes the application and the second one only runs it.

# Step 1: Restore and publish the application
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS builder
WORKDIR /app
COPY IngesCompanyCCPlatform.API/*.csproj IngesCompanyCCPlatform.API/
RUN dotnet restore ./IngesCompanyCCPlatform.API
COPY . .
RUN dotnet publish ./IngesCompanyCCPlatform.API -c Release -o out

# Step 2: Runtime image with the Production environment
FROM mcr.microsoft.com/dotnet/aspnet:10.0
ENV ASPNETCORE_ENVIRONMENT=Production
WORKDIR /app
COPY --from=builder /app/out .

# Step 3: Expose the port and run the application on the port given by the hosting provider
EXPOSE 8080
ENTRYPOINT ["sh", "-c", "ASPNETCORE_HTTP_PORTS=${PORT:-8080} exec dotnet IngesCompanyCCPlatform.API.dll"]

# Environment variables required in the hosting provider (Render) for the Production environment
# (see appsettings.Production.json):
# - DATABASE_URL: database host name or address (Railway).
# - DATABASE_PORT: database port.
# - DATABASE_NAME: database name.
# - DATABASE_USER: database user.
# - DATABASE_PASSWORD: database password.
# - PORT: port where the application listens (default 8080).
