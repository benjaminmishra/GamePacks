using GamePacks.Migrations;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class Runner : IHostedService
{
    private readonly MigrationsExecutor _migrationsExecutor;
    private readonly DataSeeder _dataSeeder;
    private readonly IHostApplicationLifetime _appLifetime;
    private readonly ILogger<Runner> _logger;

    public Runner(
        MigrationsExecutor migrationsExecutor, 
        DataSeeder dataSeeder,
        IHostApplicationLifetime appLifetime,
        ILogger<Runner> logger)
    {
        _migrationsExecutor = migrationsExecutor;
        _dataSeeder = dataSeeder;
        _appLifetime = appLifetime;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Starting migration...");
            _migrationsExecutor.ExecuteAsync(cancellationToken).Wait();
            _logger.LogInformation("Migrations successful!!");

            _logger.LogInformation("Starting data seeding...");
            await _dataSeeder.RunAsync(cancellationToken);
            _logger.LogInformation("Data Seeder successful!!");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred during migrations or data seeding.");
            Environment.Exit(-1); // Exit on failure
        }

        // Shut down the application once the tasks are done
        _appLifetime.StopApplication();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Runner is stopping.");
        return Task.CompletedTask;
    }
}
