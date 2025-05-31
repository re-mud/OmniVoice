using System.Windows.Controls;
using OmniVoice.Domain.Models;

namespace OmniVoice.Presentation.Common.IdentifiedEntities;

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
