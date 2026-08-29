# ADR-003: Module Engine for Business Modules

## Status

Accepted

## Context

Business functionality (Sales, CRM, HR, etc.) must be pluggable and independently deployable.

## Decision

Create a module engine (`IBosModule` + `ModuleEngine`) that allows business modules to register themselves and their services.

## Consequences

- Each module is self-contained
- Modules declare their own service registrations
- The API host discovers and loads modules at startup
