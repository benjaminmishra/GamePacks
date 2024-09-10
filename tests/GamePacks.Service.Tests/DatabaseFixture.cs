using DotNet.Testcontainers.Builders;
using GamePacks.DataAccess;
using GamePacks.Migrations;
using Microsoft.EntityFrameworkCore;

using Testcontainers.PostgreSql;

namespace GamePacks.Service.Tests;

public class DatabaseFixture : IAsyncLifetime
{
    private const string DatabaseName = "testdb";
    private const string UserName = "postgres";
    private const string Password = "password";
    private const int Port = 5433;

    private readonly PostgreSqlContainer _postgreSqlContainer;

    public string? ConnectionString { get; private set; }

    public DatabaseFixture()
    {
        _postgreSqlContainer = new PostgreSqlBuilder()
            .WithDatabase(DatabaseName)
            .WithUsername(UserName)
            .WithPassword(Password)
            .WithPortBinding(Port)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(Port))
            .Build();
    }

    public async Task InitializeAsync()
    {
        // Start the PostgreSQL container, wait up to 1 min to launch the container
        await _postgreSqlContainer.StartAsync(TestHelpers.CreateCancellationToken(60000));
        ConnectionString = _postgreSqlContainer.GetConnectionString();

        // Run migrations and data seeder, give them 30 sec each
        var executor = new MigrationsExecutor(GetDbContextInstance());
        executor.ExecuteAsync(TestHelpers.CreateCancellationToken(60000)).Wait();

        var dataSeeder = new DataSeeder(GetDbContextInstance());
        await dataSeeder.RunAsync(TestHelpers.CreateCancellationToken(30000));
    }

    public GamePacksDbContext GetDbContextInstance()
    {
        if (ConnectionString is null)
            throw new InvalidDataException("Connection string for database not found");

        var dbContextOptionsBuilder = new DbContextOptionsBuilder<GamePacksDbContext>();
        dbContextOptionsBuilder.UseNpgsql(ConnectionString, b => b.MigrationsAssembly("GamePacks.Migrations"));
        return new GamePacksDbContext(dbContextOptionsBuilder.Options);
    }

    public async Task DisposeAsync()
    {
        await _postgreSqlContainer.StopAsync();
    }
}