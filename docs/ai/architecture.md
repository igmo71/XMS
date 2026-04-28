# XMS Architecture Context

XMS is a .NET 10 enterprise system.

Current architecture style:
- Modular Monolith
- DDD-oriented design
- Clean Architecture principles
- Vertical Slices where appropriate
- Minimal API for backend endpoints
- Blazor Server + MudBlazor for Web UI
- No MediatR; use custom application patterns instead

## Projects

- XMS.Api
  - Backend HTTP API
  - Minimal API endpoints
  - OpenAPI / Scalar
  - Integration endpoints

- XMS.Web
  - Blazor Server UI
  - MudBlazor
  - ASP.NET Core Identity
  - Should not run background integration consumers unless explicitly required

- XMS.Application
  - Application services
  - Use cases
  - Interfaces / abstractions
  - Event bus abstractions

- XMS.Domain
  - Domain entities
  - Value objects
  - Domain rules
  - Domain events

- XMS.Infrastructure
  - EF Core
  - SQL Server persistence
  - RabbitMQ
  - Serilog
  - OpenTelemetry
  - Seq integration
  - External service implementations

- XMS.Modules
  - Functional modules
  - Module endpoint registration
  - Module-specific application logic

## Architectural Direction

XMS should evolve toward a clean modular monolith where each functional area has clear boundaries.

Potential future domain areas:
- Warehouse topology
- Receiving / inbound
- Storage
- Inventory
- Putaway
- Picking
- Shipping
- Cost allocation
- 1C integration
- Analytics

Important: LPN and UoM are planned domain concepts but are not implemented in this version yet.
