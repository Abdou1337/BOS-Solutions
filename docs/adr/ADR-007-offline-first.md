# ADR-007: Offline-First Strategy

## Status
Planned

## Context
BOS Solutions must support offline-capable desktop and future mobile clients. Changes made offline must synchronize with the cloud when connectivity is restored.

## Decision
Prepare clean boundaries for a future sync engine:
```
Local Write → Outbox → Sync Engine → Cloud → Remote Changes → Inbox → SQLite
```

Requirements for future implementation:
- Idempotency
- Retries with backoff
- Concurrency control
- Versioning
- Conflict resolution
- Tombstones
- Change tracking

## Alternatives
- Online-only (insufficient for target market)
- CRDTs (considered for specific data types)

## Consequences
- No sync engine is implemented in Phase 1
- Persistence architecture must not block future sync
- SQLite and PostgreSQL capabilities differ — no provider-specific domain assumptions
