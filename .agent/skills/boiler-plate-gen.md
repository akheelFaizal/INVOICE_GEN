---
description: "Scaffolds a new module with Clean API standards, DTOs, Mappers, and FluentValidation."
glob: "**/*.cs"
---
# Module Boilerplate Generator

When I ask you to "scaffold a new module [Name]", "create boilerplate for [Name]", or similar, perform the following actions sequentially:

## 1. Create the Folder Structure
Create the following directories for the requested `[ModuleName]`:
- `Modules/[ModuleName]/API/Controllers`
- `Modules/[ModuleName]/Core/DTOs`
- `Modules/[ModuleName]/Core/Interfaces`
- `Modules/[ModuleName]/Core/Validators`
- `Modules/[ModuleName]/Infrastructure/Data`

## 2. Generate Standard Components
Ensure all code generation adheres to the following "Clean API" standards:

- **DTOs (`Core/DTOs`)**: Create standard Request and Response DTOs. 
  - *Constraint Check*: Use `C# 10+ records` for DTOs. Apply the `decimal` constraint for any generated financial fields.
- **Validation (`Core/Validators`)**: Create a `FluentValidation.AbstractValidator<T>` for the Request DTO.
- **Interfaces (`Core/Interfaces`)**: Create a domain service or use case interface (e.g., `I[ModuleName]Service`).
- **Controller (`API/Controllers`)**: Create an API Controller inheriting from `ControllerBase`.
  - Add `[ApiController]` and `[Route("api/[controller]")]`.
  - Inject the created interface via the constructor.
  - Implement a basic POST and GET endpoint.
  - *Constraint Check*: Ensure Standardized Error Responses are used. Validation errors should return a `400 Bad Request` with `ValidationProblemDetails`, and general errors should return a standard `ProblemDetails` envelope.

## 3. Architecture Verification
Before finalizing, review the generated code to ensure NO references are made to any other existing module's `Core` or `Infrastructure` folders.
