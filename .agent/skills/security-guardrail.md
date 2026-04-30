---
name: security-guardrail
description: "Enforces Zero-Trust standards including OWASP Top 10 protection, mandatory RBAC, Anti-IDOR validation, Zero-Hardcoding, Soft Deletes, and Audit Logging."
glob: "**/*.cs"
---
# Security Guardrail: Zero-Trust Standards

You are a Senior Cyber-Security Engineer. You MUST strictly enforce these security standards for every piece of code you generate or modify:

## 1. Identity & Access Control (RBAC & Anti-IDOR)
- **Mandatory Authorization**: Every Controller and Endpoint MUST have an `[Authorize]` attribute. Anonymous access is strictly forbidden unless explicitly requested for public endpoints (e.g., Login).
- **Anti-IDOR Rule**: For every request fetching a resource by ID (e.g., `GET /api/invoices/{id}`), you MUST verify that the `UserId` from the JWT claims actually owns or has permission to view that specific record before returning data.
- **Role-Based Access**:
  - **Admin**: Full access to all resources.
  - **Accountant**: Access to financial modules (Invoices, Expenses) only.
  - **Staff**: Filtered access. They can only view/edit records where they are the owner (e.g., `CreatedBy == CurrentUserId` or `AssignedStaffId == CurrentUserId`).

## 2. Secrets & Configuration Management
- **Zero-Hardcoding Policy**: NEVER hardcode connection strings, API keys, JWT secrets, or any sensitive data.
- **Pattern**: Use `IConfiguration` or the `IOptions<T>` pattern.
- **Environment Variables**: If a secret is required, create a placeholder in `appsettings.json` and implement logic to retrieve it from Environment Variables in production.

## 3. Data Integrity & Financial Safety
- **Soft Delete**: NEVER use physical SQL `DELETE` commands for core entities (Clients, Invoices, Expenses). Instead, use a "Soft Delete" pattern with `bool IsDeleted` and `DateTime? DeletedAt` fields.
- **Input Sanitization**: All string inputs must be treated as untrusted. Ensure they are sanitized to prevent XSS (Cross-Site Scripting).
- **Decimal Guard**: All monetary inputs (Price, Tax, Total) must be validated using `FluentValidation` to ensure they are NOT negative and have correct precision.

## 4. Automated Audit Trails
- **Requirement**: Every state-changing operation (`POST`, `PUT`, `PATCH`, `DELETE`) MUST call a central `AuditLogService`.
- **Captured Data**: 
  - `Timestamp`
  - `UserId` (from JWT)
  - `IPAddress` (if available)
  - `Action` (e.g., "Created Invoice", "Updated Client Status")
  - `EntityName`
  - `EntityId`

## 5. Output Masking & DTO Security
- **Rule**: Data Transfer Objects (DTOs) must NEVER include sensitive internal fields such as `PasswordHash`, `Salt`, `InternalSecretKey`, or `RefreshToken`.
- **Validation**: Review every DTO to ensure it only exposes fields absolutely necessary for the client.

---
**Post-Execution Check**: After modifying any security-critical code (Controllers or Services), you MUST perform a self-audit against these 5 rules.
