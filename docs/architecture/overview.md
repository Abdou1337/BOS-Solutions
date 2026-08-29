# Architecture Overview

## Clean Architecture

BOS Solutions follows Clean Architecture principles with strict dependency rules:

- **Domain** → no outward dependencies
- **Application** → depends on Domain only
- **Infrastructure** → depends on Domain and Application
- **API** → depends on Application and Infrastructure

## Desktop Client

BOS.Desktop is a **Windows-only** WinUI 3 application:

- Targets `net10.0-windows10.0.22621.0`
- Uses Windows App SDK 1.7+
- Requires Windows 11 and Visual Studio 2026 Insiders
- No cross-platform or compatibility layers

## Domain Events

The domain event system is operational with an in-process dispatcher:

```
AggregateRoot.RaiseDomainEvent()
    → DomainEvents collection
        → Persistence boundary (save + collect events)
            → InProcessDomainEventDispatcher.DispatchAsync()
                → IDomainEventHandler<T>.HandleAsync()
```

## Dependency Rules (Enforced by Architecture Tests)

- Domain MUST NOT reference Application or Infrastructure
- Application MUST NOT reference Infrastructure
- These rules are validated by `BOS.Architecture.Tests`
