namespace OmniVoice.Domain.Analyzers;

public interface IParser
{
    /// <summary>
    /// basic logic of data extraction
    /// </summary>
    object ExtractData(string text);
}