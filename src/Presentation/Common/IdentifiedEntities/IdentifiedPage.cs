using System.Windows.Controls;
using OmniVoice.Domain.Command;
using OmniVoice.Domain.Models;

namespace OmniVoice.Application.Common.IdentifiedEntities;

public class IdentifiedPage : IIdentifiedEntity<Page>
{
    public string Id { get; set; }
    public Page Value { get; set; }

    public IdentifiedPage(string id, Page value)
    {
        Id = id;
        Value = value;
    }
}
