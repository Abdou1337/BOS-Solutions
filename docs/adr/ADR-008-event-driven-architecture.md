# ADR-008: Event-Driven Architecture

## Status
Accepted

## Context
The platform must support domain events for aggregate coordination, and eventually integration events for cross-module and cross-service communication.

## Decision
Establish three distinct event categories:
- **Domain Events** — raised by aggregates, dispatched in-process within a bounded context
- **Integration Events** — cross-module communication (future)
- **Realtime Notifications** — SignalR-based client notifications (future)

Current implementation:
- Domain event contracts (`IDomainEvent`, `DomainEvent`) in Domain layer
- Event dispatch contracts (`IDomainEventDispatcher`, `IDomainEventHandler<T>`) in Application layer
- In-process dispatcher implementation in Infrastructure

## Alternatives
- No event infrastructure (too limiting)
- Distributed event bus from day one (premature — no Kafka/RabbitMQ/Service Bus yet)

## Consequences
- Domain events are operational for in-process dispatch
- No outbox pattern yet
- Integration events and distributed messaging are future phases
