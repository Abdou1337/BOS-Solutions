# ADR-006: Persistence Strategy

## Status
Accepted

## Context
The platform requires both local (offline) and cloud persistence. The architecture must not block future offline-first synchronization.

## Decision
- Use EF Core 10 with PostgreSQL for cloud persistence and SQLite for local/offline persistence
- `IUnitOfWork` lives in the Application layer (not Core)
- No generic `IRepository<T>` pattern — future repositories are aggregate-specific per bounded context
- Infrastructure owns all provider-specific implementations
- Domain and Application layers have no knowledge of EF Core, SQLite, or PostgreSQL

Target architecture (future):
```
Application → Persistence Abstraction → SQLite Local / PostgreSQL Cloud → Sync Engine
```

## Alternatives
- Generic repository pattern (rejected: encourages CRUD-over-DDD)
- Dapper (considered for read-side queries in future phases)

## Consequences
- Current DbContext is minimal (no entity configurations yet)
- Repository interfaces will be created per aggregate when modules are built
- The `useSqlite` boolean in service registration is foundation-level only
