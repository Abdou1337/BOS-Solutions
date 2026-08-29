# ADR-004: Multi-Tenancy Strategy

## Status
Accepted

## Context
The platform must support multiple tenants, organizations, and companies. Users may belong to multiple tenants. Not all entities are tenant-scoped.

## Decision
Define cross-cutting tenant abstractions in Core:
- `TenantId` — strongly-typed tenant identifier
- `ITenantContext` — provides the current tenant context
- `ITenantScoped` — marker for tenant-scoped entities

The platform distinguishes:
- **Platform-scoped** entities (global system configuration)
- **Tenant-scoped** entities (isolated per tenant)
- **Organization-scoped** entities (within tenant structure)
- **Global reference data** (shared, read-only)

## Alternatives
- Single-tenant design (insufficient for target market)
- Schema-per-tenant (considered for future PostgreSQL isolation)

## Consequences
- Not all entities implement ITenantScoped
- User-tenant relationship is many-to-many (via future TenantMembership)
- Full tenant management deferred to Phase 2
