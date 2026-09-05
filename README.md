# BOS Solutions

Business Operating System Solutions — greenfield foundation for the BOS platform.

## Repository Structure

| Project | Description |
|---------|-------------|
| `src/BOS.Desktop` | Windows-only WinUI 3 desktop application (Windows App SDK) |
| `src/BOS.Infrastructure` | EF Core `BosDbContext` with domain-event persistence lifecycle |
| `src/BOS.Domain` | Domain entities, aggregates, and `IDomainEvent` abstractions |

## Prerequisites

- .NET 8 SDK
- Windows 10 1903+ / Windows 11 (for BOS.Desktop)
- Microsoft.WindowsAppSDK 1.5 (pulled automatically via NuGet)

## Build

```bash
# Cross-platform projects (Linux / macOS / Windows)
dotnet build src/BOS.Infrastructure/BOS.Infrastructure.csproj

# Desktop (Windows only)
dotnet build src/BOS.Desktop/BOS.Desktop.csproj -p:Platform=x64
```

## CI

GitHub Actions workflows are in `.github/workflows/ci.yml`.  
Linux runners build and test the cross-platform projects; Windows runners build `BOS.Desktop`.

## Domain-Event Lifecycle

`BosDbContext` dispatches domain events raised by `Entity` aggregates
immediately after `SaveChanges`/`SaveChangesAsync` commits the unit of work.
Inject an `IDomainEventDispatcher` implementation via DI to wire up event handlers.
