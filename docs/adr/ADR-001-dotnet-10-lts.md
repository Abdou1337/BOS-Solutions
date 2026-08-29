# ADR-001: .NET 10 LTS as Platform Target

## Status
Accepted

## Context
BOS Solutions requires a stable, long-term supported platform for enterprise deployment across SMEs, large enterprises, holdings, and international groups.

## Decision
Target .NET 10 LTS (SDK 10.0.400) for all projects. All NuGet packages must be compatible with .NET 10. No preview dependencies unless exceptionally justified.

## Alternatives
- .NET 9 (current, but approaching end of support)
- .NET 11 (preview, not yet LTS)

## Consequences
- Long-term support guarantee from Microsoft
- Stable API surface for enterprise customers
- All packages must be verified for .NET 10 compatibility before adoption
