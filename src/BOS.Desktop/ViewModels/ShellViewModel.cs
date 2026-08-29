using CommunityToolkit.Mvvm.ComponentModel;

namespace BOS.Desktop.ViewModels;

/// <summary>
/// Main shell view model for the desktop application.
/// Presentation-only — no business logic belongs here.
/// Future phases will add navigation, workspace management,
/// module hosting, and notification support.
/// </summary>
public partial class ShellViewModel : ObservableObject
{
    [ObservableProperty]
    private string _title = "BOS Solutions";

    [ObservableProperty]
    private bool _isLoading;
}
