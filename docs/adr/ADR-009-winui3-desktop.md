# ADR-009: WinUI 3 Desktop Architecture

## Status
Accepted

## Context
The platform includes a Windows desktop application for business users. It must support MVVM, dependency injection, and modular UI.

## Decision
- Use WinUI 3 with Windows App SDK targeting `net10.0-windows10.0.22621.0`
- Use CommunityToolkit.Mvvm for MVVM pattern
- Use Microsoft.Extensions.DependencyInjection for DI
- Desktop project is presentation-only — no business logic in ViewModels
- Desktop is built on Windows CI only (requires Windows 11 + Visual Studio 2026 Insiders)

Future desktop capabilities:
- Authentication, Workspace, Navigation, Module hosting, Notifications, Settings, Device Integration

## Alternatives
- WPF (legacy, not recommended for new development)
- Avalonia (cross-platform but less Windows-native)
- MAUI (broader platform but less desktop-focused)

## Consequences
- Desktop project requires Windows CI (windows-latest runner)
- Developers need Windows 11 + Visual Studio 2026 Insiders
- Desktop references Application layer only (not Infrastructure)
