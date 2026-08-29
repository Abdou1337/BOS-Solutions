# ADR-003: Modular Architecture

## Status
Accepted

## Context
BOS Solutions must support pluggable business modules (Catalog, Sales, CRM, HR, etc.) that can be independently developed, deployed, and versioned.

## Decision
Implement a module engine (`IBosModule` + `ModuleEngine`) that allows modules to self-register services. Each future module may own its own Domain, Application, Infrastructure, API, UI, events, permissions, localization, and migrations.

## Alternatives
- Monolithic service registration (simpler but not extensible)
- Microservices per module (too complex for initial deployment)

## Consequences
- Module engine is lightweight and does not impose business logic
- No business modules are created in the foundation phase
- Future modules register through the engine's service configuration
