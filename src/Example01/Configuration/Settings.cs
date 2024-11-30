namespace Example01.Configuration;

public class Settings
{
    public const string SectionName = "Settings";
    public int RetryCount { get; init; } = 3;
    public int RetryDelayInSeconds { get; init; } = 1;
    public int ConsumerDelayInSeconds { get; init; } = 1;
    public int ProducerDelayInSeconds { get; init; } = 5;
    public string BrokerType { get; init; } = Constants.InMemory;
    public RabbitMqSettings RabbitMq { get; init; } = new();
    public ServiceBusSettings ServiceBus { get; init; } = new();

    public class RabbitMqSettings
    {
        public string Url { get; init; } = default!;
        public string UserName { get; init; } = default!;
        public string UserPass { get; init; } = default!;
    }

    public class ServiceBusSettings
    {
        public string ConnectionString { get; init; } = default!;
    }
}