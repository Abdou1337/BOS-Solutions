# ADR-001: Windows-Only Desktop Client

## Status

Accepted

## Context

BOS Solutions requires a desktop client for Windows 11 users. The platform targets enterprise Windows environments.

## Decision

BOS.Desktop is a WinUI 3 application targeting Windows 11 exclusively:

- Target framework: `net10.0-windows10.0.22621.0`
- Windows App SDK 1.7+
- No cross-platform layers, no compatibility shims
- Builds only on Windows with Visual Studio 2026 Insiders or later

## Consequences

- Desktop client cannot be built or tested in non-Windows CI environments
- Simplifies desktop development by using native Windows APIs
- No need for abstraction layers for cross-platform support
