using System.Reflection;
using Example02.Configuration;
using Example02.Extensions;
using MassTransit;
using Microsoft.Extensions.Options;

namespace Example02;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddOpenApi(builder.Environment);
        builder.Services.Configure<Settings>(builder.Configuration.GetSection(Settings.SectionName));
        builder.Services.AddSingleton<IValidateOptions<Settings>, SettingsValidator>();
        builder.Services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();
            x.AddConsumers(Assembly.GetEntryAssembly());
            x.UsingBrokerType(builder.Configuration);
        });
        return builder;
    }
}