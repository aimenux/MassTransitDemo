namespace Example02.Extensions;

public static partial class LoggingExtensions
{
    [LoggerMessage(Level = LogLevel.Information, Message = "BrokerType is '{BrokerType}'.")]
    public static partial void LogBrokerType(this ILogger logger, string brokerType);

    [LoggerMessage(Level = LogLevel.Information, Message = "Message ({MessageId}) consumed.")]
    public static partial void LogConsumedMessage(this ILogger logger, Guid messageId);

    [LoggerMessage(Level = LogLevel.Information, Message = "Message ({MessageId}) published.")]
    public static partial void LogPublishedMessage(this ILogger logger, Guid messageId);

    public static ILogger CreateLogger<T>()
    {
        using var loggerFactory = LoggerFactory
            .Create(loggingBuilder => loggingBuilder.SetMinimumLevel(LogLevel.Trace)
                .AddConsole());
        var logger = loggerFactory.CreateLogger<T>();
        return logger;
    }
}