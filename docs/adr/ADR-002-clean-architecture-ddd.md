# ADR-002: Clean Architecture with Domain-Driven Design

## Status
Accepted

## Context
The platform must support modular business applications with clear architectural boundaries, enabling multiple teams to work on different bounded contexts independently.

## Decision
Adopt Clean Architecture with DDD principles:
- **Core**: Minimal cross-cutting platform primitives (identity, tenancy, authorization, results)
- **Domain**: DDD primitives (Entity, ValueObject, AggregateRoot, DomainEvent) and domain abstractions
- **Application**: Use cases, CQRS abstractions, event dispatch contracts, persistence abstractions
- **Infrastructure**: EF Core, external service implementations

Strict dependency rules enforced by architecture tests:
- Core → no BOS dependencies
- Domain → Core only
- Application → Domain + Core
- Infrastructure → Application (transitively Domain + Core)

## Alternatives
- N-Tier architecture (simpler but less maintainable at scale)
- Vertical slice architecture (considered for future module internals)

## Consequences
- Inner layers remain technology-independent
- DDD primitives live in Domain, not Core
- Architecture tests enforce boundary violations at build time
