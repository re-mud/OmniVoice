using OmniVoice.Domain.Services.CommandService.States;
using OmniVoice.Domain.Models;

namespace OmniVoice.Application.Models;

public class IdentifiedState : IIdentifiedEntity<ICommandServiceState>
{
    public string Id { get; set; }
    public ICommandServiceState Value { get; set; }

    public IdentifiedState(string id, ICommandServiceState value)
    {
        Id = id;
        Value = value;
    }
}
