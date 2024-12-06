using Example02.Contracts;
using MassTransit;

namespace Example02.Extensions;

public static class EndpointsExtensions
{
    public static IApplicationBuilder MapEndpoints(this WebApplication app)
    {
        app.MapPost("/api/publish", async (IBus bus, ILogger<Program> logger, CancellationToken cancellationToken) =>
        {
            var message = new Message();
            await bus.Publish(message, cancellationToken);
            logger.LogPublishedMessage(message.Id);
            return message;
        });

        return app;
    }
}