# ADR-005: Identity and Authorization Strategy

## Status
Accepted

## Context
Authentication will be handled by Supabase Auth. The platform needs clean boundaries between authentication identity, user profiles, tenant membership, roles, and permissions.

## Decision
Define identity and authorization abstractions in Core:
- `UserId` — cross-cutting user identifier
- `ICurrentUser` — provides the authenticated user context
- `IAuthorizationService` — permission-checking boundary
- `Permission` — permission descriptor

The intended authorization chain:
```
Supabase Auth → Authenticated Identity → BOS User Profile → Tenant Membership → Role → Permission → Policy
```

No custom authentication system. No password management. Supabase Auth integration deferred to Phase 2.

## Alternatives
- ASP.NET Core Identity (too coupled, doesn't align with Supabase)
- Custom JWT handling (reinventing the wheel)

## Consequences
- Current phase provides boundary abstractions only
- Authentication middleware is registered but not configured with Supabase
- Full identity implementation is Phase 2
