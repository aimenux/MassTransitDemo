using Example01.Configuration;
using MassTransit;
using Microsoft.Extensions.Options;

namespace Example01.Consumers;

public sealed class MessageConsumerDefinition : ConsumerDefinition<MessageConsumer>
{
    private readonly Settings _settings;

    public MessageConsumerDefinition(IOptions<Settings> options)
    {
        _settings = (options ?? throw new ArgumentNullException(nameof(options))).Value;
        EndpointName = Constants.EndpointName;
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<MessageConsumer> consumerConfigurator, IRegistrationContext context)
    {
        endpointConfigurator.UseMessageRetry(r => r.Interval(_settings.RetryCount, TimeSpan.FromSeconds(_settings.RetryDelayInSeconds)));
    }
}