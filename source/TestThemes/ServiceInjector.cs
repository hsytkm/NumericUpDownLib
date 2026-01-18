using MLib;
using ServiceLocator;
using Settings;

namespace TestThemes;

/// <summary>
/// Creates and initializes all services.
/// </summary>
public static class ServiceInjector
{
    /// <summary>
    /// Loads service objects into the ServiceContainer on startup of application.
    /// </summary>
    /// <returns>Returns the current <seealso cref="ServiceContainer"/> instance
    /// to let caller work with service container items right after creation.</returns>
    public static ServiceContainer InjectServices()
    {
        var appearance = AppearanceManager.GetInstance();
        ServiceContainer.Instance.AddService(SettingsManager.GetInstance(appearance.CreateThemeInfos()));
        ServiceContainer.Instance.AddService(appearance);

        return ServiceContainer.Instance;
    }
}
