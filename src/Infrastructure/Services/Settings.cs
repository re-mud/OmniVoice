using OmniVoice.Domain.Services;
using System.Runtime.Serialization;
using System.Text.Json;

namespace OmniVoice.Infrastructure.Services;

public class Settings : ISettings
{
    private Dictionary<string, string> _storage = new();

    public T? GetConfiguration<T>(string key) where T : ISerializable
    {
        if (!_storage.ContainsKey(key)) return default;

        return JsonSerializer.Deserialize<T>(_storage[key]);
    }

    public void SetConfiguration<T>(T configuration, string key) where T : ISerializable
    {
        _storage[key] = JsonSerializer.Serialize(configuration);
    }

    public void LoadFromJson(string json)
    {
        ArgumentNullException.ThrowIfNull(json, nameof(json));

        _storage = JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? throw new ArgumentNullException(nameof(json));
    }

    public string ToJson()
    {
        return JsonSerializer.Serialize(_storage);
    }
}
