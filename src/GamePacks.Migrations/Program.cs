using GamePacks.DataAccess;
using GamePacks.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

// Add Services
builder.Services.ConfigureDataAccessServices(options => builder.Configuration.GetSection(GamePacksDatabaseOptions.ConfigSection).Bind(options));
builder.Services.AddSingleton<MigrationsExecutor>();
builder.Services.AddSingleton<DataSeeder>();
builder.Services.AddHostedService<Runner>();

using var host = builder.Build();

await host.RunAsync();