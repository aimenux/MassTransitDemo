using Example01.Configuration;
using Example01.Contracts;
using Example01.Extensions;
using MassTransit;
using Microsoft.Extensions.Options;

namespace Example01.Producers;

public sealed class MessageProducer : BackgroundService
{
    private readonly IBus _bus;
    private readonly Settings _settings;
    private readonly ILogger<MessageProducer> _logger;

    public MessageProducer(IBus bus, IOptions<Settings> options, ILogger<MessageProducer> logger)
    {
        _bus = bus ?? throw new ArgumentNullException(nameof(bus));
        _settings = (options ?? throw new ArgumentNullException(nameof(options))).Value;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var message = new Message();
            await _bus.Publish(message, cancellationToken);
            _logger.LogPublishedMessage(message.Id);
            await Task.Delay(TimeSpan.FromSeconds(_settings.ProducerDelayInSeconds), cancellationToken);
        }
    }
}