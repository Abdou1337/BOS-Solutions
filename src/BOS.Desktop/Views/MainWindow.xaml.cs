using Microsoft.UI.Xaml;

namespace BOS.Desktop;

/// <summary>
/// Main application window — the platform shell.
/// Future phases will add navigation, workspace, and module hosting.
/// </summary>
public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
}
