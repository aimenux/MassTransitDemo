namespace Example01.Consumers;

public sealed class MessageConsumerException : Exception
{
    public MessageConsumerException()
    {
    }

    public MessageConsumerException(string message) : base(message)
    {
    }

    public MessageConsumerException(string message, Exception innerException) : base(message, innerException)
    {
    }
}