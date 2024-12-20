﻿using System.Reflection;
using Example01.Configuration;
using Example01.Extensions;
using Example01.Producers;
using MassTransit;
using Microsoft.Extensions.Options;

namespace Example01;

public static class DependencyInjection
{
    public static HostApplicationBuilder AddServices(this HostApplicationBuilder builder)
    {
        builder.AddSettings();
        builder.AddMessaging();
        return builder;
    }

    private static void AddSettings(this HostApplicationBuilder builder)
    {
        builder.Services.Configure<Settings>(builder.Configuration.GetSection(Settings.SectionName));
        builder.Services.AddSingleton<IValidateOptions<Settings>, SettingsValidator>();
    }

    private static void AddMessaging(this HostApplicationBuilder builder)
    {
        builder.Services.AddHostedService<MessageProducer>();
        builder.Services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();
            x.AddConsumers(Assembly.GetEntryAssembly());
            x.UsingBrokerType(builder.Configuration);
        });
    }
}