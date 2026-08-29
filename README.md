# BOS Solutions

**Business Operating System Solutions** — An enterprise platform foundation built with .NET 10 LTS.

## Architecture

Built on Clean Architecture with DDD principles:

- **BOS.Core** — Shared kernel: entities, value objects, domain events, persistence abstractions
- **BOS.Domain** — Domain models and aggregate roots
- **BOS.Application** — Application layer: CQRS abstractions, use cases
- **BOS.Infrastructure** — EF Core, identity, event dispatching
- **BOS.Api** — ASP.NET Core 10 API host with OpenAPI and SignalR
- **BOS.Desktop** — WinUI 3 desktop shell (Windows-only, CommunityToolkit.Mvvm)
- **BOS.Modules** — Pluggable module engine for business modules
- **BOS.Tests** — Architecture tests, unit tests, integration tests

## Prerequisites

- .NET 10 SDK (10.0.400+)
- Visual Studio 2026 Insiders (for WinUI 3 desktop)

## Build

```bash
dotnet restore BOS.slnx
dotnet build BOS.slnx
dotnet test BOS.slnx
```

## Technology Stack

| Layer | Technology |
|-------|-----------|
| Platform | .NET 10 LTS, C# |
| Backend | ASP.NET Core 10, OpenAPI, SignalR |
| Desktop | WinUI 3, Windows App SDK, MVVM |
| Cloud DB | Supabase / PostgreSQL |
| Local DB | SQLite |
| Auth | Supabase Auth, JWT, OAuth2/OIDC |
| ORM | Entity Framework Core 10 |
| Testing | xUnit, FluentAssertions |

## License

Proprietary — All rights reserved.
