using OmniVoice.Domain.Analyzers;
using OmniVoice.Domain.Models;

namespace OmniVoice.Application.Core.Models;

public class IdentifiedParser : IIdentifiedEntity<IParser>
{
    public string Id { get; set; }
    public IParser Value { get; set; }

    public IdentifiedParser(string id, IParser value)
    {
        Id = id;
        Value = value;
    }
}
