# Docker Setup — Local Development

## Prerequisites

- Docker installed and running
- No other services on ports 5432 (Postgres) or 5000 (API)

## Full Setup

```bash
# 1. Create shared network
docker network create salestrack-network

# 2. Run PostgreSQL
docker run --name postgres-db -p 5432:5432 \
  --network salestrack-network -d --rm \
  -e POSTGRES_PASSWORD=Abc12345 \
  -e POSTGRES_DB=dev_db \
  postgres:16

# 3. Build and run the API
docker build -t sales-track-api .
docker run -d --name salestrack-api -p 5000:8080 \
  -e ASPNETCORE_ENVIRONMENT=Development \
  -e ConnectionStrings__DefaultConnection="Host=postgres-db;Port=5432;Database=dev_db;Username=postgres;Password=Abc12345" \
  --network salestrack-network --rm sales-track-api
```

## Access

- API: `http://localhost:5000`
- Scalar API docs: `http://localhost:5000/scalar/v1` (Development only)

## Cleanup

```bash
docker stop salestrack-api postgres-db
docker network rm salestrack-network
```

## Notes

- The API container exposes port 8080 internally, mapped to 5000 externally
- Seed data (15 customers with orders) is applied automatically on startup
- Migrations are auto-applied by EF Core at startup
