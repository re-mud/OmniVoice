using OmniVoice.Domain.Command.Interfaces;
using OmniVoice.Domain.Models;

namespace OmniVoice.Application.Models;

public class IdentifiedCommand : IIdentifiedEntity<ICommand>
{
    public string Id { get; set; }
    public ICommand Value { get; set; }

    public IdentifiedCommand(string id, ICommand value)
    {
        Id = id;
        Value = value;
    }
}
