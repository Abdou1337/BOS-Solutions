# ADR-010: Solution and Repository Structure

## Status
Accepted

## Context
The repository must support enterprise-scale development with clear project boundaries and CI/CD compatibility.

## Decision
Use the modern `.slnx` solution format. Structure:
```
BOS-Solutions/
├── BOS.slnx
├── global.json
├── Directory.Build.props / .targets
├── Directory.Packages.props
├── src/
│   ├── BOS.Core/          (cross-cutting primitives)
│   ├── BOS.Domain/        (DDD primitives, domain abstractions)
│   ├── BOS.Application/   (use cases, CQRS, event/persistence contracts)
│   ├── BOS.Infrastructure/ (EF Core, external services)
│   ├── BOS.Api/           (ASP.NET Core host)
│   ├── BOS.Desktop/       (WinUI 3, Windows-only)
│   └── BOS.Modules/       (module engine)
├── tests/
│   └── BOS.Tests/
└── docs/
    ├── architecture/
    ├── adr/
    ├── development/
    └── roadmap/
```

Package versions centralized in `Directory.Packages.props`. Shared build settings in `Directory.Build.props`.

## Alternatives
- Multiple .sln files (more complex CI)
- Monorepo with multiple solutions (overkill at this stage)

## Consequences
- Single solution for all projects
- Desktop requires Windows 11 + Visual Studio 2026 Insiders to build
- Compatible with Visual Studio 2026 Insiders on Windows
