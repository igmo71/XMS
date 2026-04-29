# XMS Coding Rules (Strict)

## 1. General

* Prefer explicit over implicit
* No hidden magic
* Minimal changes per task
* Preserve existing behavior unless stated

---

## 2. Forbidden Libraries

* MediatR ❌
* AutoMapper ❌
* Heavy frameworks without approval ❌

---

## 3. Dependency Injection

### Allowed

* Explicit registrations
* Clear lifetimes

### Forbidden

* Capturing scoped services in singleton ❌
* Multiple registrations of same service (unless intentional) ❌
* Hidden side effects in extension methods ❌

---

## 4. Hosting / Startup

### Allowed in Hosting

* Serilog
* OpenTelemetry
* EF registration
* RabbitMQ wiring
* Application/Modules registration

### Forbidden

* UI config (MudBlazor, Identity) ❌
* Endpoint mapping ❌
* Middleware ordering ❌

---

## 5. Infrastructure

### Allowed

* EF Core
* RabbitMQ
* External integrations

### Forbidden

* WebApplicationBuilder usage ❌
* ASP.NET Core pipeline ❌
* Business logic ❌

---

## 6. Application Layer

### Allowed

* Use cases
* Coordination logic
* Abstractions

### Forbidden

* Direct DB access (через Infrastructure только) ❌
* ASP.NET dependencies ❌

---

## 7. Domain Layer

### Allowed

* Entities
* Value Objects
* Domain rules

### Forbidden

* EF attributes (по возможности) ❌
* External dependencies ❌
* Logging ❌

---

## 8. Events

### Rules

* Domain events ≠ Integration events
* Не смешивать

### Forbidden

* Publishing integration events из Domain ❌

---

## 9. Naming

Good:

* AddXmsHostDefaults
* AddXmsIntegrationConsumers

Bad:

* AddEverything ❌
* ConfigureAll ❌
* Utils ❌

---

## 10. Anti-patterns (critical)

* God-extension methods ❌
* "Magic" auto-discovery without control ❌
* Cross-module coupling ❌
* Silent behavior changes ❌
* Copy-paste DI ❌

---

## 11. AI Agent Rules

Before coding:

1. Read relevant files
2. Follow existing style
3. Make minimal change
4. Explain changes
5. Highlight risks

Output must include:

* Changed files
* Reasoning
* Risks
* Assumptions
