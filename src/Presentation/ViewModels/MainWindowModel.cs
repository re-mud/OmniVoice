using Microsoft.Extensions.DependencyInjection;
using OmniVoice.Application.Models;
using OmniVoice.Application.Services.CommandService;
using OmniVoice.Domain.Analyzers;
using OmniVoice.Domain.Models;
using OmniVoice.Presentation.Common.Command;
using OmniVoice.Presentation.ViewModelContracts;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace OmniVoice.Presentation.ViewModels;

public class MainWindowModel : IMainWindowModel
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public ICommand ToggleMicrophoneCommand => new RelayCommand(ToggleMicrophone);

    private CommandService _commandService;
    private IServiceProvider _serviceProvider;

    public MainWindowModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _commandService = _serviceProvider.GetRequiredService<CommandService>();

        _commandService.CommandRecognition.SetParsers(
            (IIdentifiedEntity<IParser>[])_serviceProvider.GetServices<IdentifiedParser>());
        _commandService.CommandRecognition.SetCommands(
            (IIdentifiedEntity<OmniVoice.Domain.Command.ICommand>[])_serviceProvider.GetServices<IdentifiedCommand>());

        _commandService.Start();
    }

    private void ToggleMicrophone()
    {
        if (_commandService.IsRunning)
        {
            _commandService.Stop();
        }
        else
        {
            _commandService.Start();
        }
    }

    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
}