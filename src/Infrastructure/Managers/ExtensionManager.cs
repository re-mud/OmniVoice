using System.Reflection;

using OmniVoice.Domain.Services.Logging;
using Microsoft.Extensions.Options;
using OmniVoice.Extension;
using OmniVoice.Infrastructure.Managers.Interfaces;
using OmniVoice.Infrastructure.Managers.Options;

namespace OmniVoice.Infrastructure.Managers;

public class ExtensionManager : IExtensionManager
{
    private ExtensionManagerOptions _options;
    private List<ExtensionBase> _extensions = new();
    private ILogger _logger;

    public ExtensionManager(IOptions<ExtensionManagerOptions> options, ILogger logger)
    {
        _options = options.Value;
        _logger = logger;
    }

    public void LoadExtensions()
    {
        if (!Directory.Exists(_options.ExtensionsPath)) return;

        string[] directories = Directory.GetDirectories(_options.ExtensionsPath);

        foreach (string directory in directories)
        {
            string[] files = Directory.GetFiles(directory, "*.dll");

            foreach (string file in files)
            {
                IEnumerable<ExtensionBase> extensions = LoadExtensionFromFile(file);

                foreach(ExtensionBase extension in extensions)
                {
                    AddExtension(extension);
                }
            }
        }
    }

    private IEnumerable<ExtensionBase> LoadExtensionFromFile(string filePath)
    {
        Assembly assembly;
        Type[] types;

        try
        {
            assembly = Assembly.LoadFile(Path.GetFullPath(filePath));

            types = assembly.GetTypes();
        }
        catch (Exception ex)
        {
            _logger.Warn($"Failed to load file: \"{filePath}\"");
#if DEBUG
            _logger.Warn(ex.ToString());
#endif

            yield break;
        }

        foreach (Type type in types)
        {
            if (type.IsAssignableTo(typeof(ExtensionBase)) && !type.IsAbstract)
            {
                ExtensionBase? extension = null;

                try
                {
                    extension = Activator.CreateInstance(type) as ExtensionBase;
                }
                catch (Exception ex)
                {
                    _logger.Warn($"Failed to load extension: \"{type.Name}\"");
#if DEBUG
                    _logger.Warn(ex.ToString());
#endif
                }

                if (extension != null)
                {
                    yield return extension;
                }
            }
        }

        yield break;
    }

    public ExtensionBase[] GetExtensions() => _extensions.ToArray();

    public void AddExtension(ExtensionBase extension) => _extensions.Add(extension);

    public bool RemoveExtension(ExtensionBase extension) => _extensions.Remove(extension);
}
