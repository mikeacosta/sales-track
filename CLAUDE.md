# SalesTrack — CLAUDE.md

## Overview

ASP.NET Core 10.0 Web API for customer and order management. PostgreSQL backend, deployed to AWS via Docker/ECR with GitHub Actions CI/CD.

## Tech Stack

| Layer | Technology |
|-------|-----------|
| Runtime | .NET 10.0, C# (nullable refs, implicit usings) |
| Web | ASP.NET Core 10.0 |
| ORM | EF Core 10.0 with Npgsql (PostgreSQL 16) |
| DB naming | snake_case via EFCore.NamingConventions |
| API docs | OpenAPI + Scalar (Laserwave theme) |
| Testing | xUnit 2.9, Moq 4.20, coverlet |
| Infra | Docker, Terraform (AWS), GitHub Actions |

## Project Structure

```
SalesTrack.sln
├── SalesTrack.API/          # Main API project
│   ├── Controllers/         # HTTP endpoints
│   ├── Services/            # Business logic (interfaces + implementations)
│   ├── Repositories/        # Data access (interfaces + implementations)
│   ├── Data/                # AppDbContext, SeedData
│   ├── Entities/            # Domain models (Customer, Order)
│   ├── DTOs/                # Request/response shapes
│   ├── Mappers/             # Manual Entity↔DTO mapping
│   ├── Migrations/          # EF Core migrations (gitignored)
│   └── Program.cs           # DI registration, middleware
├── SalesTrack.Test/         # Unit tests
├── IaC/                     # Terraform (AWS RDS, SSM)
├── Dockerfile               # Multi-stage build (SDK → runtime)
└── .github/workflows/       # CI/CD pipeline
```

## Build / Test / Run

```bash
# Restore & build
dotnet restore
dotnet build

# Run tests (matches CI config)
dotnet test SalesTrack.Test/SalesTrack.Test.csproj --configuration Release --verbosity detailed

# Run locally
dotnet run --project SalesTrack.API

# Docker (see docs/docker-setup.md for full network setup)
docker build -t sales-track-api .
```

## Architecture

**Controller → Service → Repository → DbContext**

- All layers use interface-based DI (scoped lifetime; mapper is singleton)
- Fully async (`Task<T>`) throughout
- Repositories use `AsNoTracking()` for reads, `Include()` for eager loading
- Controllers return proper status codes: 200, 201 (`CreatedAtAction`), 400, 404

## Coding Conventions

- **Naming**: PascalCase for classes/methods, `I` prefix for interfaces, snake_case in DB
- **DTOs**: Separate Create/Update/Response DTOs per entity
- **Returns**: `IReadOnlyList<T>` for collections
- **Tests**: Arrange-Act-Assert with Moq; real `Mapper` instances (not mocked)
- **XML comments**: `/// <summary>` on controller actions for OpenAPI docs
- **DateTimeOffset**: UTC-normalized via EF value converter

## CI/CD Pipeline

Triggered on push to `main` or manual dispatch:
1. Restore → Run unit tests (gate) → Docker build → Push to AWS ECR
2. Images tagged with `latest` + commit SHA
3. AWS auth via OIDC (no long-lived secrets)

## Workflow Rules

- **Tests must pass** before any Docker build (enforced in CI)
- **Migrations are gitignored** — generate from code, don't commit
- **`terraform.tfvars` is gitignored** — never commit secrets
- **`dev-notes.md` is gitignored** — local-only scratch notes
- **Connection strings** set via environment variables, not in source
- Follow the existing Controller/Service/Repository pattern when adding features
- Add unit tests for new service methods (mock repositories, use real mappers)

## Detailed Docs

- [Docker Setup](docs/docker-setup.md) — local dev with Docker network
- [Infrastructure](docs/infrastructure.md) — Terraform modules and AWS resources
