using GamePacks.DataAccess;
using GamePacks.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using GamePacks.Migrations;

var builder = Host.CreateApplicationBuilder(args);

// Add Services
builder.Services.ConfigureDataAccessServices(options => builder.Configuration.GetSection(GamePacksDatabaseOptions.ConfigSection).Bind(options));
builder.Services.AddSingleton<MigrationsExecutor>();
builder.Services.AddSingleton<DataSeeder>();
builder.Services.AddHostedService<Runner>();

using var host = builder.Build();

await host.RunAsync();