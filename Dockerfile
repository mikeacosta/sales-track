# -------------------------
# Build stage
# -------------------------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy solution and project files
COPY SalesTrack.sln .
COPY SalesTrack.API/SalesTrack.API.csproj SalesTrack.API/

# Restore dependencies
RUN dotnet restore SalesTrack.API/SalesTrack.API.csproj

# Copy everything else
COPY . .

# Publish the API
RUN dotnet publish SalesTrack.API/SalesTrack.API.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

# -------------------------
# Runtime stage
# -------------------------
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Copy published output
COPY --from=build /app/publish .

# ASP.NET Core settings
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
# 4022 is JetBrains Rider's default .NET Core debugger port on Linux
EXPOSE 4022   

# Run the app
ENTRYPOINT ["dotnet", "SalesTrack.API.dll"]
