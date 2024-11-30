using Example02;
using Example02.Extensions;

var builder = WebApplication
    .CreateBuilder(args)
    .AddServices();

var app = builder.Build();

app.UseOpenApi();
app.UseHttpsRedirection();
app.MapEndpoints();

await app.RunAsync();