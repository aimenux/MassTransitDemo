using Example01;

var builder = Host
    .CreateApplicationBuilder(args)
    .AddServices();

var host = builder.Build();

await host.RunAsync();