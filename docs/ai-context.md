# XMS AI Context

## Architecture

* Modular Monolith
* DDD + Clean Architecture
* Vertical Slices
* No MediatR (custom dispatcher/pipeline)

## Projects

* XMS.Api (Minimal API)
* XMS.Web (Blazor Server + MudBlazor + Identity)
* XMS.Application
* XMS.Domain
* XMS.Infrastructure
* XMS.Modules

## Rules

* Avoid code duplication
* Keep separation of concerns
* Do not mix UI/API logic into shared infrastructure
* Integration consumers must NOT run in Web project
* Prefer explicit DI over magic

## Infrastructure

* EF Core (DbContextFactory)
* RabbitMQ (integration events)
* OpenTelemetry + Seq
* Serilog

## Goal

Keep architecture clean, extensible and production-ready
