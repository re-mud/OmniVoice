using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OmniVoice.Domain.Services;
using OmniVoice.Infrastructure.Services;
using OmniVoice.Infrastructure.Services.Logging;
using OmniVoice.Infrastructure.Services.Logging.Options;
using NAudio.Wave;

using OmniVoice.Infrastructure.Services.Options;
using Microsoft.Extensions.Options;
using OmniVoice.Domain.SpeechRecognition;
using OmniVoice.Domain.Microphone;

namespace OmniVoice.Infrastructure.Configuration;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Configure options
        services.Configure<LoggerOptions>(configuration.GetSection("Logger"));
        services.Configure<VoskModelOptions>(configuration.GetSection("VoskModel"));
        services.Configure<VoskSpeechRecognitionOptions>(configuration.GetSection("VoskSpeechRecognition"));
        services.Configure<WaveFormatOptions>(configuration.GetSection("WaveFormat"));

        // Register options
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<LoggerOptions>>().Value);
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<VoskModelOptions>>().Value);
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<VoskSpeechRecognitionOptions>>().Value);
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<WaveFormatOptions>>().Value);

        // Register services
        services.AddSingleton<ILogger, Logger>();

        services.AddTransient(sp => CreateVoskModel(sp, configuration));
        services.AddTransient<IMicrophone, NAudioMicrophone>(sp => CreateMicrophone(sp, configuration));
        services.AddTransient<ISpeechRecognition, VoskSpeechRecognition>();

        return services;
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
