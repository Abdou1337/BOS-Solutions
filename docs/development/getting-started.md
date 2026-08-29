# Development Guide

## Getting Started

1. Install .NET 10 SDK (10.0.400+)
2. Clone the repository
3. Run `dotnet restore BOS.slnx`
4. Run `dotnet build BOS.slnx`
5. Run `dotnet test BOS.slnx`

## Project Structure

| Project | Purpose |
|---------|---------|
| BOS.Core | Shared kernel primitives |
| BOS.Domain | Domain entities and aggregates |
| BOS.Application | Use cases, CQRS handlers |
| BOS.Infrastructure | EF Core, external services |
| BOS.Api | ASP.NET Core API host |
| BOS.Desktop | WinUI 3 desktop application |
| BOS.Modules | Module engine abstractions |
| BOS.Tests | All test types |

## Conventions

- File-scoped namespaces
- Nullable reference types enabled
- Treat warnings as errors
- Architecture tests enforce Clean Architecture dependency rules
