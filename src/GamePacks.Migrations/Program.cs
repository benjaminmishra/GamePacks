using GamePacks.DataAccess;
using GamePacks.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using GamePacks.Migrations;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.ConfigureDataAccessServices(
    options => builder.Configuration.GetSection(GamePacksDatabaseOptions.ConfigSection).Bind(options));
builder.Services.AddSingleton<MigrationsExecutor>();

using var host = builder.Build();

var executor = host.Services.GetService<MigrationsExecutor>() ?? throw new InvalidOperationException($"Could not find service of type {nameof(MigrationsExecutor)}");

try
{
    executor.Execute();
    Console.WriteLine("Migrations successful!!");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    throw;
}

await host.RunAsync();