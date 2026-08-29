# BOS Solutions

Business Operating System Solutions — greenfield foundation for the BOS Solutions platform.

## Architecture

Clean Architecture with four layers:

| Layer | Project | Purpose |
|---|---|---|
| Domain | `BOS.Domain` | Entities, value objects, domain events, aggregate roots |
| Application | `BOS.Application` | Use cases, interfaces, event dispatcher contracts |
| Infrastructure | `BOS.Infrastructure` | Implementations (persistence, event dispatching) |
| API | `BOS.API` | ASP.NET Core Web API host |
| Desktop | `BOS.Desktop` | WinUI 3 desktop client (Windows 11 only) |

## Desktop Client

`BOS.Desktop` is a **Windows-only** WinUI 3 application targeting:

- **Windows 11** (10.0.22621.0+)
- **.NET 10** (`net10.0-windows10.0.22621.0`)
- **Windows App SDK 1.7+**
- **Visual Studio 2026 Insiders** or later

> **Note:** BOS.Desktop cannot be built on non-Windows environments. CI builds exclude it from restore/build on non-Windows runners.

## Domain Events

The domain event lifecycle is **operational** (in-process):

1. **Aggregate** raises events via `RaiseDomainEvent()`
2. Events accumulate in `AggregateRoot.DomainEvents`
3. **Persistence boundary** — events are collected before clearing
4. **Dispatcher** (`InProcessDomainEventDispatcher`) resolves handlers from DI
5. **Handlers** (`IDomainEventHandler<T>`) process each event

## Prerequisites

- .NET 10 SDK
- Windows 11 + Visual Studio 2026 Insiders (for BOS.Desktop)

## Build

```bash
# Restore and build all non-Windows projects
dotnet build BOS.slnx

# Run tests
dotnet test BOS.slnx
```

## Solution Structure

```
BOS.slnx
├── src/
│   ├── BOS.Domain/          # Domain layer
│   ├── BOS.Application/     # Application layer
│   ├── BOS.Infrastructure/  # Infrastructure layer
│   ├── BOS.API/             # Web API host
│   └── BOS.Desktop/         # WinUI 3 client (Windows only)
├── tests/
│   ├── BOS.Domain.Tests/    # Domain + event lifecycle tests
│   └── BOS.Architecture.Tests/ # Clean architecture dependency tests
└── docs/
    ├── architecture/        # Architecture documentation
    └── adr/                 # Architecture Decision Records
```
