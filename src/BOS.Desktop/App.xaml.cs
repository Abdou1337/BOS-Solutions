// BOS.Desktop - WinUI 3 Application Entry Point
// This project targets Windows 11 with Windows App SDK.
// It requires Visual Studio 2026 Insiders or later to build.

#if WINDOWS
using Microsoft.UI.Xaml;

namespace BOS.Desktop;

public partial class App : Application
{
    public App()
    {
        this.InitializeComponent();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        // Main window initialization will be added here.
    }
}
#endif
