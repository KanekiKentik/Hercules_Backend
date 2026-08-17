using Testcontainers.PostgreSql;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using Respawn;
public class IntegrationTestsFixture : IAsyncLifetime
{
    public PostgreSqlContainer Database { get; private set; } = null!;
    public WebApplicationFactory<Program> Factory { get; private set; } = null!;
    public HttpClient Client { get; private set; } = null!;
    public DbConnection DbConnection { get; private set; } = null!;
    internal HerculesContext Context { get; private set; } = null!;
    private Respawner Respawner { get; set; } = null!;
    private IServiceScope _scope = null!;
    public async Task RespawnDb()
    {
        await Respawner.ResetAsync(DbConnection);
    }
    public virtual async Task InitializeAsync()
    {
        Database = new PostgreSqlBuilder("postgres:latest")
            .WithDatabase("test_db")
            .WithUsername("postgre")
            .WithPassword("postgre")
            .Build();

        await Database.StartAsync();

        Factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
               builder.ConfigureServices(services =>
               {
                   var descriptor = services.Single(d => d.ServiceType == typeof(HerculesContext));
                   services.Remove(descriptor);

                   services.AddDbContext<HerculesContext>(options =>
                       options.UseNpgsql(Database.GetConnectionString()));
               });
            });

        Client = Factory.CreateClient();

        _scope = Factory.Services.CreateScope();
        Context = _scope.ServiceProvider.GetRequiredService<HerculesContext>();
        //await Context.Database.MigrateAsync();

        DbConnection = Context.Database.GetDbConnection();
        await DbConnection.OpenAsync();

        Respawner = await Respawner.CreateAsync(DbConnection);
    }
    public async Task DisposeAsync()
    {
        _scope.Dispose();
        await Database.DisposeAsync();
    }
}