namespace OmniVoice.Domain.Analyzers;

public interface IParser
{
    float Probability(string text);
    object ExtractData(string text);
}