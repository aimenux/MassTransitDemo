using Microsoft.Extensions.Options;

namespace Example01.Configuration;

public sealed class SettingsValidator : IValidateOptions<Settings>
{
    public ValidateOptionsResult Validate(string? name, Settings? options)
    {
        if (options is null)
        {
            return ValidateOptionsResult.Fail($"{nameof(Settings)} is required.");
        }

        if (options.RetryCount <= 0)
        {
            return ValidateOptionsResult.Fail($"{nameof(Settings.RetryCount)} must be greater than 0.");
        }

        if (options.RetryDelayInSeconds <= 0)
        {
            return ValidateOptionsResult.Fail($"{nameof(Settings.RetryDelayInSeconds)} must be greater than 0.");
        }

        if (options.ConsumerDelayInSeconds <= 0)
        {
            return ValidateOptionsResult.Fail($"{nameof(Settings.ConsumerDelayInSeconds)} must be greater than 0.");
        }

        if (options.ProducerDelayInSeconds <= 0)
        {
            return ValidateOptionsResult.Fail($"{nameof(Settings.ProducerDelayInSeconds)} must be greater than 0.");
        }

        if (!IsValidBrokerType(options.BrokerType))
        {
            return ValidateOptionsResult.Fail($"{nameof(Settings.BrokerType)} is invalid.");
        }

        if (options.BrokerType == Constants.RabbitMq && !IsValidRabbitMqSettings(options.RabbitMq))
        {
            return ValidateOptionsResult.Fail($"{nameof(Settings.RabbitMq)} is invalid.");
        }

        if (options.BrokerType == Constants.ServiceBus && !IsValidServiceBusSettings(options.ServiceBus))
        {
            return ValidateOptionsResult.Fail($"{nameof(Settings.ServiceBus)} is invalid.");
        }

        return ValidateOptionsResult.Success;
    }

    private static bool IsValidBrokerType(string brokerType)
    {
        return brokerType is Constants.InMemory or Constants.RabbitMq or Constants.ServiceBus;
    }

    private static bool IsValidRabbitMqSettings(Settings.RabbitMqSettings settings)
    {
        return !string.IsNullOrWhiteSpace(settings.Url) && !string.IsNullOrWhiteSpace(settings.UserName) && !string.IsNullOrWhiteSpace(settings.UserPass);
    }

    private static bool IsValidServiceBusSettings(Settings.ServiceBusSettings settings)
    {
        return !string.IsNullOrWhiteSpace(settings.ConnectionString);
    }
}