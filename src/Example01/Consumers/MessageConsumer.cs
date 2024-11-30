using System.Security.Cryptography;
using Example01.Configuration;
using Example01.Contracts;
using Example01.Extensions;
using MassTransit;
using Microsoft.Extensions.Options;

namespace Example01.Consumers;

public sealed class MessageConsumer : IConsumer<Message>
{
    private readonly Settings _settings;
    private readonly ILogger<MessageConsumer> _logger;

    public MessageConsumer(IOptions<Settings> options, ILogger<MessageConsumer> logger)
    {
        _settings = (options ?? throw new ArgumentNullException(nameof(options))).Value;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Consume(ConsumeContext<Message> context)
    {
        await Task.Delay(TimeSpan.FromSeconds(_settings.ConsumerDelayInSeconds), context.CancellationToken);

        if (IsFailure())
        {
            throw new MessageConsumerException($"Failed to process message ({context.Message.Id}).");
        }

        _logger.LogConsumedMessage(context.Message.Id);
    }

    private static bool IsFailure()
    {
        return RandomNumberGenerator.GetBytes(1)[0] % 5 == 0;
    }
}