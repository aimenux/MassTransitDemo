namespace Example02.Extensions;

public static class OpenApiExtensions
{
    public static void AddOpenApi(this IServiceCollection services, IWebHostEnvironment environment)
    {
        if (environment.IsProduction())
        {
            return;
        }

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public static void UseOpenApi(this WebApplication app)
    {
        if (app.Environment.IsProduction())
        {
            return;
        }

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.DisplayRequestDuration();
        });
    }
}