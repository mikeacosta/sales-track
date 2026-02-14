# Infrastructure — Terraform / AWS

## Overview

Infrastructure is managed with Terraform in the `IaC/` directory. All resources deploy to **us-west-2**.

## Provider

- **Terraform**: >= 1.5.0
- **AWS Provider**: ~> 5.0

## Modules

### `rds-postgres`

Located at `IaC/modules/rds-postgres/`. Provisions:

| Resource | Details |
|----------|---------|
| RDS Instance | PostgreSQL 16, `db.t3.micro`, 20 GB storage |
| SSM Parameter | Connection string stored as SecureString at `/salestrack/{env}/connection-string` |

**Variables**: `db_name`, `db_username`, `db_password` (set via `terraform.tfvars`, which is gitignored).

Uses default VPC and security group.

## Environments

Defined in `IaC/envs/dev/`:

```bash
cd IaC/envs/dev
terraform init
terraform plan
terraform apply
```

## CI/CD Integration

The GitHub Actions workflow authenticates to AWS via OIDC (role-based, no long-lived keys) and pushes Docker images to ECR. The ECR repository and OIDC role are assumed to exist outside this Terraform config.

## Security

- `terraform.tfvars` is gitignored — never commit database credentials
- Connection strings are stored in AWS SSM Parameter Store as SecureString
- The API reads its connection string from environment variables at runtime
