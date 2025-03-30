namespace OmniVoice.Domain.Services;

public interface IClassRegistry
{
    void AddElement<T>(string key, T element) where T : class;
    T? GetElement<T>(string key) where T : class;
    void RemoveElement(string key);
    T[] GetElements<T>() where T : class;
    string[] GetKeys<T>() where T : class;
    Dictionary<string, T> GetKeysWithElements<T>() where T : class;
}