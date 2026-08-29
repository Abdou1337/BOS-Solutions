# Architecture Overview

BOS Solutions follows **Clean Architecture** with **Domain-Driven Design (DDD)** principles.

## Layer Dependencies

```
Core (cross-cutting primitives — no BOS dependencies)
  ↑
Domain (DDD primitives — depends on Core only)
  ↑
Application (use cases, CQRS, event/persistence contracts — depends on Domain + Core)
  ↑
Infrastructure (EF Core, external services — depends on Application)
  ↑
API / Desktop (presentation — depends on Infrastructure or Application)
```

Modules (module engine) are independent — they depend only on DI abstractions.

## Status Legend

- **Implemented**: Code exists and is functional
- **Prepared**: Abstractions/boundaries exist, implementation deferred
- **Planned**: Documented in roadmap, no code yet

## Core Layer (Implemented)

Cross-cutting platform primitives only:
- `UserId` / `ICurrentUser` — identity boundary
- `TenantId` / `ITenantContext` / `ITenantScoped` — multi-tenancy boundary
- `Permission` / `IAuthorizationService` — authorization boundary
- `Result` / `Result<T>` — operation result types

## Domain Layer (Implemented)

DDD primitives:
- `EntityId` — strongly-typed identifier base
- `Entity<TId>` — base entity with domain event support
- `IAggregateRoot` — aggregate root marker
- `IDomainEvent` / `DomainEvent` — domain event contracts
- `ValueObject` — value object base

No business aggregates yet — those belong in future bounded context modules.

## Application Layer (Implemented)

- `ICommand` / `ICommandHandler<T>` — CQRS command contracts
- `IQuery<T>` / `IQueryHandler<T, TResult>` — CQRS query contracts
- `IDomainEventDispatcher` / `IDomainEventHandler<T>` — event dispatch contracts
- `IUnitOfWork` — persistence coordination

## Infrastructure Layer (Implemented)

- `BosDbContext` — EF Core context (foundation-level, no entity configs yet)
- `DomainEventDispatcher` — in-process event dispatcher
- `CurrentUser` / `TenantContext` — default identity/tenancy implementations
- Service registration with SQLite/PostgreSQL provider selection

## API Layer (Implemented)

- ASP.NET Core 10 host with OpenAPI and SignalR
- Health check endpoint
- Authentication/authorization middleware registered (Prepared — Supabase integration deferred)

## Desktop Layer (Implemented)

- Real WinUI 3 / Windows App SDK project configuration
- App.xaml / MainWindow shell
- CommunityToolkit.Mvvm with DI
- Requires Windows 11 + Visual Studio 2026 (excluded from Linux CI)

## Module Engine (Implemented)

- `IBosModule` — module contract
- `ModuleEngine` — module registration and service configuration
