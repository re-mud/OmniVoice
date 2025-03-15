namespace OmniVoice.Domain.Analyzers;

public interface IParser
{
    /// <summary>
    /// the probability of containing data in a row is subtracted
    /// </summary>
    float Probability(string text);
    /// <summary>
    /// basic logic of data extraction
    /// </summary>
    object ExtractData(string text);
}