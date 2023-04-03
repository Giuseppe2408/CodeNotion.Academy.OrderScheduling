using System.Data.Common;
using CodeNotion.Academy.OrderScheduling.Data;
using CodeNotion.Academy.OrderScheduling.Models;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using DotNet.Testcontainers.Builders;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Respawn;
using Testcontainers.MsSql;

namespace CodeNotion.Academy.OrderScheduling.Tests;

public class OrderApiFactory : WebApplicationFactory<OrderDbContext>, IAsyncLifetime
{
    private DbConnection _dbConnection = default!;
    private Respawner _respawner = default!;

    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder()
        .WithPassword("yourStrong(!)Password")
        .WithCleanUp(true)
        .Build();
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(OrderDbContext));
            services.AddTransient<OrderDbContext>(sp => new OrderDbContext(new DbContextOptionsBuilder<OrderDbContext>()
                .UseSqlServer(_dbContainer.GetConnectionString())
                .Options));
        });
    }

    public async Task ResetDatabaseAsync()
    {
        await _respawner.ResetAsync(_dbConnection);
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        _dbConnection = new SqlConnection(_dbContainer.GetConnectionString());
        await _dbConnection.OpenAsync();
        await InitializeRespawner();
    }

    private async Task InitializeRespawner()
    {
        _respawner = await Respawner.CreateAsync(_dbConnection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.SqlServer,
            SchemasToInclude = new[] { "public" }
        });
    }

    public new async Task DisposeAsync() => await _dbContainer.DisposeAsync();
}