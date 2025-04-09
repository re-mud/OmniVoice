using System.Runtime.Serialization;

namespace OmniVoice.Domain.Services;

public interface ISettings
{
    T? GetConfiguration<T>(string key) where T : ISerializable;
    void SetConfiguration<T>(T configuration, string key) where T : ISerializable;
    /// <exception cref="ArgumentNullException"></exception>
    void LoadFromJson(string json);
    string ToJson();
}
