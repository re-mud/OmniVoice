using OmniVoice.Domain.Services;

namespace OmniVoice.Infrastructure.Services;

public class ClassRegistry : IClassRegistry
{
    private readonly Dictionary<Type, Dictionary<string, object>> _storage = new();

    public void AddElement<T>(string key, T element) where T : class
    {
        if (key == null) throw new ArgumentNullException(nameof(key));
        if (element == null) throw new ArgumentNullException(nameof(element));

        var type = typeof(T);
        if (!_storage.ContainsKey(type))
        {
            _storage[type] = new Dictionary<string, object>();
        }

        _storage[type][key] = element;
    }

    public T? GetElement<T>(string key) where T : class
    {
        if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));

        var type = typeof(T);
        if (!_storage.ContainsKey(type)) return null;
        if (!_storage[type].ContainsKey(key)) return null;

        return _storage[type][key] as T;
    }

    public void RemoveElement(string key)
    {
        if (key == null) throw new ArgumentNullException(nameof(key));

        foreach (var typeDict in _storage.Values)
        {
            typeDict.Remove(key);
        }
    }

    public T[] GetElements<T>() where T : class
    {
        var type = typeof(T);
        if (!_storage.ContainsKey(type)) return Array.Empty<T>();

        return _storage[type].Values.Cast<T>().ToArray();
    }

    public string[] GetKeys<T>() where T : class
    {
        var type = typeof(T);
        if (!_storage.ContainsKey(type)) return Array.Empty<string>();

        return _storage[type].Keys.ToArray();
    }
}
