# Architecture Overview

BOS Solutions follows **Clean Architecture** with **Domain-Driven Design (DDD)** principles.

## Layer Dependencies

```
BOS.Api ──► BOS.Infrastructure ──► BOS.Application ──► BOS.Domain ──► BOS.Core
BOS.Modules ──────────────────────► BOS.Application ──► BOS.Domain ──► BOS.Core
BOS.Desktop ──────────────────────► BOS.Application ──► BOS.Domain ──► BOS.Core
```

## Core Primitives (BOS.Core)

- `Entity<TId>` — Base entity with domain event support
- `EntityId` — Strongly-typed identifier base
- `ValueObject` — Value object base
- `IDomainEvent` / `DomainEvent` — Domain event contracts
- `IRepository<T, TId>` — Generic repository interface
- `IUnitOfWork` — Unit of work abstraction
- `ITenantContext` / `ITenantScoped` — Multi-tenancy abstractions
- `ICurrentUser` / `UserId` — Identity boundaries
- `IAuthorizationService` / `Permission` — Authorization boundaries
- `IDomainEventDispatcher` / `IDomainEventHandler<T>` — Event infrastructure
- `Result` / `Result<T>` — Operation result types

## Module Engine (BOS.Modules)

- `IBosModule` — Module contract for pluggable business modules
- `ModuleEngine` — Discovers, registers, and configures modules

## Key Patterns

- **CQRS**: `ICommand` / `IQuery<T>` with corresponding handlers
- **DDD**: Aggregates, entities, value objects, domain events
- **Multi-Tenancy**: Tenant-scoped entities and context
- **Clean Architecture**: Enforced via architecture tests
