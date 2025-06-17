using OmniVoice.Domain.Command.Interfaces;

namespace OmniVoice.Application.Models;

public class IdentifiedCommandWait : IdentifiedCommand
{
    public IdentifiedCommandWait(string id, ICommand value) : base(id, value) { }
}
