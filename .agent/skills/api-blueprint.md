---
name: api-blueprint
description: "Architectural standard for Modular Monolith in ASP.NET Core, enforcing strict decoupling, financial safety, and standard response patterns."
glob: "**/*.cs"
---
# API Blueprint: Modular Monolith Standards

You are a Senior .NET Solution Architect. Every time you create or modify a module, service, or endpoint, you MUST adhere to these mandatory standards:

## 1. Folder Mapping Rules
Each module MUST follow this exact 4-layer structure:
- **Core**: `Modules/[ModuleName]/Core/`
  - *Contents*: Domain Entities, Domain Interfaces, Domain Exceptions, Value Objects.
- **Application**: `Modules/[ModuleName]/Application/`
  - *Contents*: DTOs (Requests/Responses), Service Logic (Interfaces & Impl), FluentValidators, Mappers (AutoMapper/Mapster).
- **Infrastructure**: `Modules/[ModuleName]/Infrastructure/`
  - *Contents*: EF Core DbContext/Configs, Repository Implementations, External API Clients, File Storage.
- **API**: `Modules/[ModuleName]/API/`
  - *Contents*: Controllers, Middleware specific to the module.

## 2. Coding Standards
- **Financial Safety**: Strictly use `decimal` for all currency fields (`Price`, `Tax`, `Total`, `Balance`, etc.). Never use `float` or `double`.
- **Decoupling**: Modules MUST communicate via shared Interfaces or MediatR Events. NO project references to another module's internal layers (`Application`, `Infrastructure`).
- **Response Wrapping**: All API responses MUST use a consistent `Result<T>` or `Envelope<T>` pattern. 
  - Success: `{ "data": T, "success": true }`
  - Failure: `{ "error": "Message", "success": false, "details": [] }`
- **Async First**: All I/O-bound methods (DB, API, File) must use `async/await`.

## 3. RBAC (Role-Based Access Control)
Apply `[Authorize]` attributes to all controllers or specific endpoints:
- **Admin**: Full access to all endpoints.
- **Accountant**: Access to financial management (Invoices, Expenses).
- **Staff**: Access to their own records only (logic must be handled in the service layer).

## 4. Post-Execution Hook
Whenever you modify a `.csproj` file or add a new service/module, you MUST automatically execute the dependency checker to ensure no architectural violations were introduced:
- Command: `& .\.agent\skills\check-dependencies.ps1` (or `sh ./.agent/skills/check-dependencies.sh`)

## 5. Verification
Before responding, perform a self-audit:
1. Is the file in the correct layer?
2. Are all monetary fields `decimal`?
3. Is the response wrapped in a `Result<T>`?
4. Are RBAC attributes applied?
