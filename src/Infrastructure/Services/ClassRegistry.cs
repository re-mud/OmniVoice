using OmniVoice.Domain.Services;

namespace OmniVoice.Infrastructure.Services;

public class ClassRegistry : IClassRegistry
{
    private Dictionary<string, object?> _data = new();

    public void AddElement<T>(string key, T element) where T : class
    {
        _data[key] = element;
    }

    public T? GetElement<T>(string key) where T : class
    {
        _data.TryGetValue(key, out var value);
        return value as T;
    }

    public T[] GetElements<T>() where T : class
    {
        return _data.Values.OfType<T>().ToArray();
    }

    public string[] GetKeys<T>() where T : class
    {
        return _data.Where(kv => kv.Value is T).Select(kv => kv.Key).ToArray();
    }

    public void RemoveElement(string key)
    {
        _data.Remove(key);
    }
}
