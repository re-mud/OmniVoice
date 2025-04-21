using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using OmniVoice.Domain.Microphone.Interfaces;
using OmniVoice.Domain.Services;
using OmniVoice.Domain.SpeechRecognition.Interfaces;
using OmniVoice.Infrastructure.Services;
using OmniVoice.Infrastructure.Services.Logging;
using OmniVoice.Infrastructure.Services.Logging.Options;
using NAudio.Wave;

using OmniVoice.Infrastructure.Services.Options;

namespace OmniVoice.Infrastructure.Configuration;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<LoggerOptions>(configuration.GetSection("Logger"));
        services.Configure<VoskModelOptions>(configuration.GetSection("VoskModel"));
        services.Configure<VoskSpeechRecognitionOptions>(configuration.GetSection("VoskSpeechRecognition"));
        services.Configure<WaveFormatOptions>(configuration.GetSection("WaveFormat"));

        services.AddSingleton<ILogger, Logger>();

        services.AddTransient(sp => CreateVoskModel(sp, configuration));
        services.AddTransient<IMicrophone, NAudioMicrophone>(sp => CreateMicrophone(sp, configuration));
        services.AddTransient<ISpeechRecognition, VoskSpeechRecognition>();

        return services;
    }

    private static Vosk.Model CreateVoskModel(IServiceProvider sp, IConfiguration configuration)
    {
        var options = configuration.GetRequiredSection("WaveInEvent").Get<VoskModelOptions>();

        return new Vosk.Model(options.ModelPath);
    }

    private static NAudioMicrophone CreateMicrophone(IServiceProvider sp, IConfiguration configuration)
    {
        var options = configuration.GetSection("WaveInEvent").Get<WaveFormatOptions>() ?? new WaveFormatOptions();
        var waveIn = new WaveInEvent();
        waveIn.WaveFormat = new WaveFormat(options.SampleRate, options.Bits, options.Channels);

        return new NAudioMicrophone(waveIn);
    }
}
