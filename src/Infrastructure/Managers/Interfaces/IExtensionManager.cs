using Microsoft.Extensions.DependencyInjection;

using OmniVoice.Extension;

namespace OmniVoice.Infrastructure.Managers.Interfaces;

public interface IExtensionManager
{
    public void LoadExtensions();
    public void RegistrationServices(IServiceCollection serviceCollection);
    public ExtensionBase[] GetExtensions();
    public void AddExtension(ExtensionBase extension);
    public bool RemoveExtension(ExtensionBase extension);
}