# -----------------------------
# Stage 1: Build
# -----------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy solution and project files
COPY VehicleInventory.sln ./
COPY VehicleInventory.API/VehicleInventory.API.csproj VehicleInventory.API/
COPY VehicleInventory.Application/VehicleInventory.Application.csproj VehicleInventory.Application/
COPY VehicleInventory.Domain/VehicleInventory.Domain.csproj VehicleInventory.Domain/
COPY VehicleInventory.Infrastructure/VehicleInventory.Infrastructure.csproj VehicleInventory.Infrastructure/

# Restore dependencies
RUN dotnet restore

# Copy all files
COPY . ./

# Publish API project
WORKDIR /app/VehicleInventory.API
RUN dotnet publish -c Release -o out

# -----------------------------
# Stage 2: Runtime
# -----------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/VehicleInventory.API/out ./

# Expose port
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

# Run the API
ENTRYPOINT ["dotnet", "VehicleInventory.API.dll"]