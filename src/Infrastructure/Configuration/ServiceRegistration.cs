using System.Speech.Synthesis;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

using NAudio.Wave;

using OmniVoice.Domain.Services.SpeechRecognition;
using OmniVoice.Domain.Services.SpeechSynthesizer;
using OmniVoice.Domain.Services.Microphone;
using OmniVoice.Domain.Services.Logging;
using OmniVoice.Infrastructure.Services.Logging.Options;
using OmniVoice.Infrastructure.Managers.Interfaces;
using OmniVoice.Infrastructure.Managers.Options;
using OmniVoice.Infrastructure.Services.Options;
using OmniVoice.Infrastructure.Services.Logging;
using OmniVoice.Infrastructure.Managers;
using OmniVoice.Infrastructure.Services;

namespace OmniVoice.Infrastructure.Configuration;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        AddOptions(services, configuration);
        AddServices(services, configuration);

        return services;
    }

    private static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ILogger, Logger>();
        services.AddSingleton<IExtensionManager, ExtensionManager>();

        services.AddTransient<Vosk.Model>(sp => CreateVoskModel(sp, configuration));
        services.AddTransient<IMicrophone, NAudioMicrophone>(sp => CreateMicrophone(sp, configuration));
        services.AddTransient<ISpeechRecognition, VoskSpeechRecognition>();
        services.AddTransient<ISpeechSynthesizer, WindowsSpeechSynthesizer>();

        services.AddTransient<SpeechSynthesizer>();
    }

    private static void AddOptions(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<LoggerOptions>(configuration.GetSection("Logger"));
        services.Configure<VoskModelOptions>(configuration.GetSection("VoskModel"));
        services.Configure<VoskSpeechRecognitionOptions>(configuration.GetSection("VoskSpeechRecognition"));
        services.Configure<WaveFormatOptions>(configuration.GetSection("WaveFormat"));
        services.Configure<ExtensionManagerOptions>(configuration.GetSection("ExtensionManager"));

        services.AddSingleton(sp => sp.GetRequiredService<IOptions<LoggerOptions>>().Value);
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<VoskModelOptions>>().Value);
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<VoskSpeechRecognitionOptions>>().Value);
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<WaveFormatOptions>>().Value);
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<ExtensionManagerOptions>>().Value);
    }

    private static Vosk.Model CreateVoskModel(IServiceProvider sp, IConfiguration configuration)
    {
        var options = configuration.GetRequiredSection("VoskModel").Get<VoskModelOptions>();

        return new Vosk.Model(options.ModelPath);
    }

    private static NAudioMicrophone CreateMicrophone(IServiceProvider sp, IConfiguration configuration)
    {
        var options = sp.GetRequiredService<IOptions<WaveFormatOptions>>().Value;
        var waveIn = new WaveInEvent();
        waveIn.WaveFormat = new WaveFormat(options.SampleRate, options.Bits, options.Channels);

        return new NAudioMicrophone(waveIn);
    }
}
