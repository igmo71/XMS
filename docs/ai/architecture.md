# XMS Architecture (Formal)

## 1. System Style

* Modular Monolith
* DDD-oriented
* Clean Architecture
* Vertical Slices (без MediatR; собственный dispatcher/pipeline)

## 2. Top-level Components

* **XMS.Api** — HTTP API (Minimal API), integration endpoints
* **XMS.Web** — Blazor Server UI (MudBlazor, Identity)
* **XMS.Hosting** — composition root (startup, wiring, observability)
* **XMS.Application** — use cases, application services, abstractions
* **XMS.Domain** — domain model, invariants, domain events
* **XMS.Infrastructure** — EF Core, RabbitMQ, external integrations
* **XMS.Modules** — функциональные модули (feature boundaries)

---

## 3. Dependency Rules (Allowed)

```
Api/Web -> Hosting
Hosting -> Application
Hosting -> Infrastructure
Hosting -> Modules

Application -> Domain
Infrastructure -> Application (abstractions) + Domain

Modules -> Application + Domain (+ Infrastructure, если необходимо)
```

## 4. Forbidden Dependencies

* Domain -> Infrastructure ❌
* Domain -> ASP.NET Core ❌
* Application -> Web/UI ❌
* Infrastructure -> Web/UI ❌
* Infrastructure -> Hosting ❌

---

## 5. Composition Root

**Only in XMS.Hosting:**

* Serilog configuration
* OpenTelemetry configuration
* DI wiring (Application, Infrastructure, Modules)
* RabbitMQ publisher/consumers registration (осознанно)

**Program.cs (Api/Web):**

* минимальный bootstrap
* middleware pipeline
* endpoint mapping
* UI/Identity (только Web)

---

## 6. Modules (Bounded Contexts)

Each module:

* имеет свои endpoints (через `MapModulesEndpoints`)
* использует Application слой
* не шарит внутренние детали других модулей

Примеры будущих модулей:

* Receiving
* Storage
* Inventory
* Picking
* Shipping
* Cost Allocation
* Integration (1C)

---

## 7. Events

* **Domain Events** — внутри Domain/Application
* **Integration Events** — через RabbitMQ

Consumers:

* регистрируются ТОЛЬКО там, где это осознанно нужно (обычно Api/worker)
* НЕ в Web по умолчанию

---

## 8. Observability

* Конфигурация — в Hosting
* Infrastructure содержит только low-level integration (если требуется)

---

## 9. Future Domain Concepts

(пока не реализованы, но зарезервированы)

* LPN (License Plate)
* UoM (Units of Measure)
* Inventory model
* Slotting / routing

---

## 10. Design Principles

* Явные зависимости
* Минимум магии
* Читаемый startup
* Изоляция слоёв
* Расширяемость без рефакторинга ядра
