# Task: Refactor Hosting Configuration

## Context

Use:

* /docs/ai/architecture.md
* /docs/ai/coding-rules.md

The solution contains duplicated startup configuration in:

* XMS.Api/Program.cs
* XMS.Web/Program.cs

---

## Problem

The following configuration is duplicated:

* Serilog setup
* OpenTelemetry
* EF Core (DbContextFactory)
* RabbitMQ (connection factory, publisher)
* EventBus
* ApplicationServices
* Modules
* IntegrationServices

This violates DRY and makes maintenance harder.

---

## Task

Refactor startup configuration into shared hosting extensions.

---

## Requirements

1. Create new extension class:

* Namespace: `XMS.Infrastructure.Hosting`
* File: `XmsHostApplicationBuilderExtensions.cs`

2. Implement methods:

### AddXmsHostDefaults(builder, serviceName)

Must include:

* Serilog configuration
* AddAppPersistenceInfrastructure
* AddAppOpenTelemetry
* EventBus registration
* RabbitMQ connection + publisher
* ApplicationServices
* Modules
* IntegrationServices

---

### AddXmsIntegrationConsumers(builder)

Must include:

* Assembly scan: assemblies starting with "XMS."
* Register integration event handlers
* Register RabbitMQ consumer as HostedService

---

## Refactoring

### XMS.Api

Replace duplicated configuration with:

builder
.AddXmsHostDefaults("XMS.Api")
.AddXmsIntegrationConsumers();

---

### XMS.Web

Replace duplicated configuration with:

builder
.AddXmsHostDefaults("XMS.Web");

---

## Constraints

* Do NOT register Integration Consumers in Web
* Do NOT move UI-specific configuration (MudBlazor, Identity)
* Do NOT change behavior
* Do NOT introduce new libraries
* Follow existing naming and DI style

---

## Expected Output

* New extension class (full code)
* Updated Program.cs (Api + Web)
* List of modified files
* Explanation of changes

---

## Review Checklist

* No duplication remains
* Clear separation between shared and app-specific config
* No unintended side effects
* No duplicate RabbitMQ consumers
* Code is readable and explicit
