# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Kopiramo samo .csproj i restore-ujemo dependencije
COPY Services/Services.csproj ./Services/
RUN dotnet restore ./Services/Services.csproj

# Kopiramo ostatak
COPY . .

WORKDIR /app/Services
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "Services.dll"]
