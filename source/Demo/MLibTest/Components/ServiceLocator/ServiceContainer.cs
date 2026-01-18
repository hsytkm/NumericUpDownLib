namespace ServiceLocator;

/// <summary>
/// Source: http://www.codeproject.com/Articles/70223/Using-a-Service-Locator-to-Work-with-MessageBoxes
/// </summary>
public sealed class ServiceContainer
{
    public static ServiceContainer Instance { get; } = new();

    private readonly Dictionary<Type, object> _serviceMap = [];
    private readonly object _serviceMapLock = new();

    public void AddService<TServiceContract>(TServiceContract implementation)
        where TServiceContract : class
    {
        lock (_serviceMapLock)
        {
            _serviceMap[typeof(TServiceContract)] = implementation;
        }
    }

    public TServiceContract? GetService<TServiceContract>()
        where TServiceContract : class
    {
        object? service = null;
        lock (_serviceMapLock)
        {
            _serviceMap.TryGetValue(typeof(TServiceContract), out service);
        }
        return service as TServiceContract;
    }
}
