# XMS

XMS — это решение на **.NET 10** для интеграции внутренних справочников и внешних систем (1С, AD, Bitrix, Yunu), состоящее из:

- `XMS.Web` — Blazor Server UI.
- `XMS.Api` — минимальный HTTP API для модулей интеграции.
- `XMS.Application` — прикладная логика и контракты.
- `XMS.Domain` — доменные модели.
- `XMS.Infrastructure` — EF Core, интеграционные клиенты, observability.
- `XMS.Modules` — подключаемые бизнес-модули (сейчас `GodooModule`).

## Технологический стек

- ASP.NET Core (`net10.0`)
- Blazor Server + MudBlazor
- Entity Framework Core (SQL Server)
- ASP.NET Core Identity
- Serilog + Seq
- OpenTelemetry (OTLP exporter)

## Структура репозитория

```text
XMS.slnx
├── XMS.Api/
├── XMS.Web/
├── XMS.Application/
├── XMS.Domain/
├── XMS.Infrastructure/
└── XMS.Modules/
```

## Требования

1. Установленный **.NET SDK 10.0**.
2. Доступ к SQL Server.
3. (Опционально) Seq для централизованных логов.
4. Доступность интеграционных endpoint'ов (1С/AD/Bitrix/Yunu) или тестовые заглушки.

## Быстрый старт

### 1) Клонирование

```bash
git clone https://github.com/igmo71/XMS.git
cd XMS
```

### 2) Настройка конфигурации

В проектах используются `appsettings.json`, `appsettings.Development.json` и user secrets.

Минимально требуется строка подключения БД:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=XMS;User Id=sa;Password=Your_password123;TrustServerCertificate=true"
  }
}
```

Рекомендуется хранить секреты (пароли, API-ключи) через user secrets/переменные окружения, а не в `appsettings.json`.

### 3) Применение миграций

```bash
dotnet ef database update --project XMS.Infrastructure --startup-project XMS.Web
```

> Если вы запускаете API как startup-проект, можно использовать `--startup-project XMS.Api`.

### 4) Запуск Web

```bash
dotnet run --project XMS.Web
```

По умолчанию профили запуска используют:
- `http://localhost:5101`
- `https://localhost:7012`

### 5) Запуск API

```bash
dotnet run --project XMS.Api
```

По умолчанию:
- `http://localhost:5152`
- `https://localhost:7243`

OpenAPI/Scalar поднимаются при старте приложения.

## Конфигурация интеграций

В решении используются секции:

- `BuhClientConfig`
- `ZupClientConfig`
- `UtClientConfig`
- `AdClientConfig`
- `BitrixClientConfig`
- `GodooBuhClientConfig`
- `YunuClientConfig`

Для `OneS`-клиентов обязательны `BaseAddress`, `ServiceUri`, `UserName`, `Password`.
Для `YunuClientConfig.ApiKeys` ожидаются элементы с `Name`, `Value`, `CompanyId`.

## Наблюдаемость

- Логи через Serilog (console + Seq).
- Трейсы через OpenTelemetry OTLP (endpoint вычисляется из конфигурации Serilog Seq URL).

## Основные API эндпоинты (модуль Godoo)

- `GET /api/godoo/marketplace-relations/reload/godoo`
- `GET /api/godoo/marketplace-relations/reload/ip`
- `GET /integration/yunu/article-relations/godoo`
- `GET /integration/yunu/article-relations/ip`

## Docker

Для `XMS.Web` и `XMS.Api` есть `Dockerfile`. Можно собрать образы отдельно:

```bash
docker build -t xms-web -f XMS.Web/Dockerfile .
docker build -t xms-api -f XMS.Api/Dockerfile .
```
