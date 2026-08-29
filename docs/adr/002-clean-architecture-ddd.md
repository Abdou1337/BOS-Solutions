# ADR-002: Clean Architecture with DDD

## Status

Accepted

## Context

The platform must support modular business applications with clear boundaries.

## Decision

Adopt Clean Architecture with DDD principles:
- Core layer contains shared primitives (no external dependencies)
- Domain layer contains business entities and rules
- Application layer contains use cases and CQRS abstractions
- Infrastructure layer implements persistence and external services

## Consequences

- Strict dependency rules enforced by architecture tests
- Inner layers cannot reference outer layers
- All external concerns are abstracted behind interfaces
