using OmniVoice.Domain.Command.Interfaces;

namespace OmniVoice.Application.Core.Models;

public class IdentifiedCommandWait : IdentifiedCommand
{
    public IdentifiedCommandWait(string id, ICommand value) : base(id, value) { }
}
