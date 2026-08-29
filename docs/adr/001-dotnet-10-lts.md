# ADR-001: Use .NET 10 LTS as Platform Target

## Status

Accepted

## Context

BOS Solutions requires a stable, long-term supported platform for enterprise deployment.

## Decision

Target .NET 10 LTS (SDK 10.0.400) for all projects.

## Consequences

- Long-term support guarantee from Microsoft
- All packages must be compatible with .NET 10
- No preview dependencies unless absolutely necessary
