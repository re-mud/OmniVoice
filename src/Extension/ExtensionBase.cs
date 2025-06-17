using Microsoft.Extensions.DependencyInjection;

namespace OmniVoice.Extension;

public abstract class ExtensionBase
{
    public abstract string Name { get; }
    public abstract string Version { get; }
    public abstract string Description { get; }

    public abstract void RegisterServices(IServiceCollection services);
}
