# Task: Extract Hosting Layer from Infrastructure

## Context

Use:

* /docs/ai/architecture.md
* /docs/ai/coding-rules.md

Current state:

* Hosting-related code (WebApplicationBuilder extensions) is located in:
  XMS.Infrastructure/Hosting

* This includes:

  * Serilog configuration
  * OpenTelemetry configuration
  * Application composition (Application, Modules, Integration)

This violates separation of concerns:
Infrastructure should not depend on ASP.NET Core hosting abstractions.

---

## Problem

`XMS.Infrastructure` currently contains code that depends on:

* Microsoft.AspNetCore.Builder (WebApplicationBuilder)
* Hosting configuration logic

This mixes:

* Infrastructure (EF, RabbitMQ, etc.)
  with
* Application composition / startup orchestration

---

## Goal

Extract hosting/composition logic into a separate project:

XMS.Hosting

---

## Task

### 1. Create new project

* Name: `XMS.Hosting`
* Target: net10.0
* Project type: Class Library

---

### 2. Move files

Move:

* XMS.Infrastructure/Hosting/XmsHostApplicationBuilderExtensions.cs

To:

* XMS.Hosting/Hosting/XmsHostApplicationBuilderExtensions.cs

---

### 3. Fix dependencies

`XMS.Hosting` should reference:

* XMS.Infrastructure
* XMS.Application
* XMS.Modules

`XMS.Infrastructure` must NOT reference XMS.Hosting.

---

### 4. Clean up Infrastructure

Remove:

* Hosting folder from XMS.Infrastructure

Ensure Infrastructure contains only:

* Persistence (EF Core)
* Messaging (RabbitMQ)
* Observability primitives (if low-level)
* External integrations

---

### 5. Refactor Observability (important)

Move **hosting-level observability configuration**:

* AddAppOpenTelemetry(...)
* Serilog setup

To XMS.Hosting

Keep in Infrastructure only:

* low-level telemetry helpers (if any)

---

### 6. Keep API and Web unchanged

Ensure:

XMS.Api:

builder
.AddXmsHostDefaults("XMS.Api")
.AddXmsIntegrationConsumers();

XMS.Web:

builder
.AddXmsHostDefaults("XMS.Web");

Behavior must remain identical.

---

## Constraints

* Do NOT change behavior
* Do NOT introduce new libraries
* Do NOT break DI registrations
* Do NOT move EF / RabbitMQ implementations out of Infrastructure
* Do NOT move UI-related code

---

## Expected Output

* New project: XMS.Hosting
* Updated project references
* Moved extension class
* Cleaned Infrastructure project
* Build must succeed

---

## Review Checklist

* Infrastructure no longer depends on ASP.NET Core hosting
* Hosting layer depends on Infrastructure (not vice versa)
* Observability setup is located in Hosting
* No behavior regression
* Solution structure is clean and logical
