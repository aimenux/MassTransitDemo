using Example01.Configuration;
using MassTransit;
using static Example01.Extensions.LoggingExtensions;

namespace Example01.Extensions;

public static class MassTransitExtensions
{
    private static readonly ILogger Logger = CreateLogger<Program>();

    public static void UsingBrokerType(this IBusRegistrationConfigurator configurator, IConfiguration configuration)
    {
        var settings = configuration.GetSettings();

        Logger.LogBrokerType(settings.BrokerType);

        switch (settings.BrokerType)
        {
            case Constants.InMemory:
                configurator.UsingInMemory((context, cfg) =>
                {
                    cfg.ConfigureEndpoints(context);
                });
                break;
            case Constants.RabbitMq:
                configurator.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(settings.RabbitMq.Url, "/", host =>
                    {
                        host.Username(settings.RabbitMq.UserName);
                        host.Password(settings.RabbitMq.UserPass);
                    });
                    cfg.ConfigureEndpoints(context);
                });
                break;
            case Constants.ServiceBus:
                configurator.UsingAzureServiceBus((context, cfg) =>
                {
                    cfg.Host(settings.ServiceBus.ConnectionString);
                    cfg.ConfigureEndpoints(context);
                });
                break;
            default:
                throw new ArgumentException($"Invalid broker type '{settings.BrokerType}'.");
        }
    }

    private static Settings GetSettings(this IConfiguration configuration)
    {
        var settings = new Settings();
        configuration.GetSection(Settings.SectionName).Bind(settings);
        return settings;
    }
}