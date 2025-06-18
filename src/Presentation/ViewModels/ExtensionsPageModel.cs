using OmniVoice.Extension;
using OmniVoice.Infrastructure.Managers.Interfaces;
using OmniVoice.Presentation.Common.Views;
using OmniVoice.Presentation.Models;
using OmniVoice.Presentation.ViewModelContracts;
using System.Collections.ObjectModel;

namespace OmniVoice.Presentation.ViewModels;

public class ExtensionsPageModel : ViewModelBase, IExtensionsPageModel
{
    public ExtensionBase[] Extensions { get => _extensionManager.GetExtensions(); }

    private IExtensionManager _extensionManager;

    public ExtensionsPageModel(IExtensionManager extensionManager)
    {
        _extensionManager = extensionManager;

        _extensionManager.LoadExtensions();
    }
}
