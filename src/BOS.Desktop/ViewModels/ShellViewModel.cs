using CommunityToolkit.Mvvm.ComponentModel;

namespace BOS.Desktop.ViewModels;

/// <summary>
/// Main shell view model for the desktop application.
/// </summary>
public partial class ShellViewModel : ObservableObject
{
    [ObservableProperty]
    private string _title = "BOS Solutions";

    [ObservableProperty]
    private bool _isLoading;
}
