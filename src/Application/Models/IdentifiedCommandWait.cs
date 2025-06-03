using OmniVoice.Domain.Command;

namespace OmniVoice.Application.Models;

public class IdentifiedCommandWait : IdentifiedCommand
{
    public IdentifiedCommandWait(string id, ICommand value) : base(id, value) { }
}
