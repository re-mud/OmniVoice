namespace OmniVoice.Domain.Models;

public interface IIdentifiedEntity<T>
{
    string Id { get; set; }
    T Value { get; set; }
}