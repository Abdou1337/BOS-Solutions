# Development Guide

## Prerequisites

- .NET 10 SDK (10.0.400+)
- For Desktop: Windows 11 + Visual Studio 2026 Insiders

## Quick Start

```bash
dotnet restore BOS.slnx
dotnet build BOS.slnx
dotnet test BOS.slnx
```

Note: `BOS.Desktop` (WinUI 3) is part of the `BOS.slnx` solution and is validated on Windows GitHub Actions CI.

## Project Structure

| Project | Layer | Purpose |
|---------|-------|---------|
| BOS.Core | Core | Cross-cutting primitives (identity, tenancy, auth, results) |
| BOS.Domain | Domain | DDD primitives (Entity, ValueObject, AggregateRoot, DomainEvent) |
| BOS.Application | Application | CQRS, event/persistence contracts, DI registration |
| BOS.Infrastructure | Infrastructure | EF Core, external service implementations |
| BOS.Api | Presentation | ASP.NET Core 10 API host |
| BOS.Desktop | Presentation | WinUI 3 desktop application (Windows-only) |
| BOS.Modules | Cross-cutting | Module engine abstractions |
| BOS.Tests | Tests | Architecture, unit, and integration tests |

## Conventions

- File-scoped namespaces
- Nullable reference types enabled
- Treat warnings as errors
- Architecture tests enforce Clean Architecture dependency rules
- No business modules in the foundation phase
