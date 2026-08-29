# BOS Solutions Roadmap

## Phase 1 — Platform Foundation ✅

- [x] .NET 10 LTS project structure
- [x] Clean Architecture layers with dependency enforcement
- [x] DDD primitives in Domain layer
- [x] CQRS abstractions
- [x] Multi-tenant boundaries
- [x] Identity and authorization boundaries
- [x] Persistence boundaries (IUnitOfWork in Application)
- [x] Event infrastructure (domain event contracts + in-process dispatcher)
- [x] Module engine
- [x] API shell (ASP.NET Core, OpenAPI, SignalR)
- [x] Desktop shell (WinUI 3, MVVM, DI)
- [x] Architecture tests
- [x] 10 ADRs documented

## Phase 2 — Identity + Tenant + Authorization

- [ ] Supabase Auth integration
- [ ] JWT token handling and validation
- [ ] User profile model
- [ ] Tenant management
- [ ] Tenant membership (users ↔ tenants)
- [ ] Role-based access control
- [ ] Permission system

## Phase 3 — Persistence Foundation

- [ ] Entity configurations per bounded context
- [ ] Migration strategy
- [ ] Repository implementations per aggregate
- [ ] Read-side query support

## Phase 4 — Offline Synchronization

- [ ] SQLite local persistence
- [ ] Outbox/Inbox patterns
- [ ] Sync engine
- [ ] Conflict resolution

## Phase 5 — Module Engine Enhancement

- [ ] Module lifecycle management
- [ ] Module dependency resolution
- [ ] Module permission registration
- [ ] Module localization hooks

## Phase 6 — Dynamic Workspace

- [ ] Desktop navigation framework
- [ ] Module UI hosting
- [ ] Workspace management

## Phase 7 — Catalog Module

## Phase 8 — Sales Module

## Phase 9 — Purchasing Module

## Phase 10 — Inventory Module

## Phase 11 — Accounting Module

## Phase 12 — CRM / HR / Supply Chain

## Phase 13 — Reporting / BI

## Phase 14 — Device Hub

## Phase 15 — Data Integration Hub

## Phase 16 — Marketplace

## Phase 17 — AI Capabilities

## Phase 18 — Production / Scale / Observability
