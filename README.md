# BOS Solutions

**Business Operating System Solutions** — An enterprise platform foundation built with .NET 10 LTS.

## Status

**Phase 1 — Platform Foundation** (current)

| Capability | Status |
|---|---|
| Clean Architecture | Implemented |
| DDD Primitives | Implemented |
| CQRS Abstractions | Implemented |
| Multi-Tenancy Boundaries | Prepared |
| Identity Boundaries | Prepared |
| Authorization Boundaries | Prepared |
| Persistence Boundaries | Prepared |
| Event Infrastructure | Implemented (in-process) |
| Module Engine | Implemented |
| API Shell | Implemented |
| Desktop Shell | Implemented (Windows-only) |
| Supabase Auth | Planned (Phase 2) |
| Offline Sync | Planned (Phase 4) |

## Prerequisites

- .NET 10 SDK (10.0.400+)
- For Desktop: Windows 11 + Visual Studio 2026 Insiders

## Build

```bash
dotnet restore BOS.slnx
dotnet build BOS.slnx
dotnet test BOS.slnx
```

## Architecture

```
Core (cross-cutting primitives)
  ↑
Domain (DDD primitives)
  ↑
Application (use cases, CQRS, contracts)
  ↑
Infrastructure (EF Core, implementations)
  ↑
API / Desktop (presentation)
```

See [docs/architecture/overview.md](docs/architecture/overview.md) for details.

## Technology Stack

| Layer | Technology |
|-------|-----------|
| Platform | .NET 10 LTS, C# |
| Backend | ASP.NET Core 10, OpenAPI, SignalR |
| Desktop | WinUI 3, Windows App SDK, CommunityToolkit.Mvvm |
| Cloud DB | PostgreSQL (via Supabase) |
| Local DB | SQLite |
| Auth | Supabase Auth (planned) |
| ORM | Entity Framework Core 10 |
| Testing | xUnit, FluentAssertions |

## Documentation

- [Architecture Overview](docs/architecture/overview.md)
- [Architecture Decision Records](docs/adr/)
- [Development Guide](docs/development/getting-started.md)
- [Roadmap](docs/roadmap/roadmap.md)
