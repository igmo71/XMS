# XMS Coding Rules for AI Agents

## General Rules

- Keep code explicit and readable.
- Avoid unnecessary abstractions.
- Avoid third-party libraries unless explicitly approved.
- Do not introduce MediatR.
- Do not introduce AutoMapper.
- Prefer native .NET / ASP.NET Core / EF Core capabilities.
- Keep behavior backward-compatible unless the task explicitly requires a breaking change.

## Dependency Direction

Allowed direction:

Api/Web -> Application -> Domain
Api/Web -> Infrastructure
Infrastructure -> Application abstractions / Domain
Modules -> Application / Domain / Infrastructure where currently required

Avoid:
- Domain depending on Infrastructure
- Domain depending on ASP.NET Core
- Application depending directly on Web UI
- Shared infrastructure becoming a dumping ground

## Startup / Hosting Rules

Shared startup configuration should be placed in hosting extensions when it is common to multiple applications.

Good examples:
- Serilog setup
- OpenTelemetry setup
- EF Core persistence registration
- RabbitMQ connection factory
- Application services registration
- Module registration

Do not move app-specific configuration into shared extensions.

Keep in Program.cs:
- Blazor / UI setup
- Identity setup
- OpenAPI / Scalar setup
- Endpoint mapping
- Middleware pipeline ordering when app-specific

Important:
- Integration event consumers must not be registered in XMS.Web by default.
- Consumers should be registered explicitly only in the process that is intended to consume messages.

## Naming

Prefer names that describe intent, not implementation details.

Examples:
- AddXmsHostDefaults
- AddXmsIntegrationConsumers
- MapModulesEndpoints
- AddApplicationModules

Avoid names like:
- AddEverything
- ConfigureAll
- CommonStuff
- Utils

## EF Core

- Prefer explicit configurations.
- Be careful with tracking behavior.
- Use DbContextFactory where the project already uses it.
- Do not silently change persistence behavior.
- Avoid enabling sensitive logging in production-specific code.

## Events / Integration

Distinguish:
- Domain events: internal business events
- Integration events: cross-boundary / external messages

RabbitMQ consumers should be registered intentionally.
Do not start duplicate consumers in multiple host applications unless the task explicitly requires scale-out behavior.

## AI Agent Behavior

Before changing code:
1. Read relevant files.
2. Preserve existing style.
3. Make minimal necessary changes.
4. Explain changed files.
5. Mention risks or assumptions.
6. Ensure code compiles logically.
