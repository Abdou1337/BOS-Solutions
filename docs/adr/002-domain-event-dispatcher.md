# ADR-002: In-Process Domain Event Dispatcher

## Status

Accepted

## Context

Domain events must be dispatched after the persistence boundary to notify handlers of state changes.

## Decision

Use an in-process domain event dispatcher (`InProcessDomainEventDispatcher`) that resolves handlers from the DI container.

Lifecycle:
1. Aggregate raises events via `RaiseDomainEvent()`
2. Persistence layer saves changes and collects events
3. Dispatcher iterates events and resolves `IDomainEventHandler<T>` from DI
4. Handlers execute sequentially

## Consequences

- Simple, testable, no external dependencies
- Handlers run in the same process and transaction context
- Can be replaced with an out-of-process dispatcher (e.g., message bus) later
