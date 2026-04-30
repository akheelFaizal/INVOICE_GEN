---
description: "Triggers when creating a new module or adding endpoints to enforce Modular Monolith architecture rules."
glob: "**/*.cs"
---
# Modular Monolith Architecture Guardrails

When I ask you to create a new module, add a new endpoint, or write architectural code, you MUST strictly verify your output against these rules before responding:

1. **Strict Decoupling**: Modules (e.g., Invoicing, Expenses) must reside in separate folders/projects. They must NEVER reference each other's internal `Services` or `Data` layers directly. Inter-module communication must happen exclusively via shared `Interfaces` or asynchronous `Events`.
2. **Folder Structure**: Every module MUST follow this exact structure: `Modules/[ModuleName]/[API | Core | Infrastructure]`. Do not deviate from this naming convention.
3. **Data Integrity**: NO cross-module SQL joins are allowed. Each module must own and manage its own database tables.
4. **Financial Safety**: Strictly use `decimal` for all currency, pricing, and financial fields. NEVER use `float` or `double`.
