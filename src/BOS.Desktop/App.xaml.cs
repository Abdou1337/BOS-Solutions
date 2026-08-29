using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;

namespace BOS.Desktop;

/// <summary>
/// BOS Solutions desktop application entry point.
/// Configures DI, navigation, and the application shell.
/// </summary>
public partial class App : Application
{
    private Window? _window;

    public IServiceProvider Services { get; private set; } = default!;

    public App()
    {
        InitializeComponent();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        Services = serviceCollection.BuildServiceProvider();

        _window = new MainWindow();
        _window.Activate();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        // Future: register application services, view models, navigation
        services.AddTransient<ViewModels.ShellViewModel>();
    }
}
