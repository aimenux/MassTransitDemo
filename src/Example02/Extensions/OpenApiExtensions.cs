namespace Example02.Extensions;

public static class OpenApiExtensions
{
    public static void AddOpenApi(this WebApplicationBuilder builder)
    {
        if (builder.Environment.IsProduction())
        {
            return;
        }

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
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