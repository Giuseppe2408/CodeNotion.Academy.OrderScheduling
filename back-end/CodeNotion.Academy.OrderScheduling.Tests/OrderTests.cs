using CodeNotion.Academy.OrderScheduling.Cqrs.Commands;
using CodeNotion.Academy.OrderScheduling.Data;
using CodeNotion.Academy.OrderScheduling.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CodeNotion.Academy.OrderScheduling.Tests;

public class OrderTests : IClassFixture<OrderApiFactory>, IAsyncLifetime
{
    private readonly IServiceProvider _provider;
    private readonly IMediator _mediator; 
    private readonly OrderApiFactory _orderTestBase;
    private readonly Func<Task> _resetDatabase;

    public OrderTests(OrderApiFactory factory)
    {
        var services = new ServiceCollection();
        services.AddDbContext<OrderDbContext>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
        _provider = services.BuildServiceProvider().CreateScope().ServiceProvider;
        _mediator = _provider.GetRequiredService<IMediator>();
        _orderTestBase = factory;
        var scope = factory.Services.CreateScope();
        _resetDatabase = factory.ResetDatabaseAsync;
    }
    
    [Fact]
    public async Task Should_CreateOrder_When_OrderIsCorrect()
    {
        // Arrange
        var order = new Order
        {
            Customer = "test",
            OrderNumber = "123"
        };

        // Act
        var result = await _mediator.Send(new CreateOrderCommand(order));
        
        // Assert
        Assert.NotNull(result);
        Assert.True(result.Id > 0);
        Assert.Equal(order.Customer, result.Customer);
        Assert.Equal(order.OrderNumber, result.OrderNumber);
    }

    [Fact]
    public async Task Should_UpdateOrder_When_OrderIsCorrect()
    {
        // Arrange
        var order = _provider
            .GetRequiredService<OrderDbContext>()
            .Orders
            .AsNoTracking()
            .OrderByDescending(x => x.Id).FirstOrDefault();

        if (order != null)
        {
            var newOrder = new Order
            {
                Id = order.Id,
                Customer = "test",
                OrderNumber = "1279"
            };

            // Act
            var result = await _mediator.Send(new UpdateOrderCommand(newOrder));

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Id == order.Id);
            Assert.Equal(newOrder.Customer, result.Customer);
            Assert.Equal(newOrder.OrderNumber, result.OrderNumber);
        }
    }

    [Fact]
    public async Task Should_DeleteOrder_When_OrderExists()
    {
        // Arrange
        var order = _provider
            .GetRequiredService<OrderDbContext>()
            .Orders
            .AsNoTracking()
            .OrderByDescending(x => x.Id).FirstOrDefault();


        if (order != null)
        {
            // Act
            var result = await _mediator.Send(new DeleteOrderCommand(order.Id));

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Id == order.Id);
        }
    }

    [Fact]
    public async Task Should_NotThrowError_When_DeletingNonExistingOrder()
    {
        // Arrange
        var order = _provider
            .GetRequiredService<OrderDbContext>()
            .Orders
            .AsNoTracking()
            .OrderByDescending(x => x.Id).FirstOrDefault();

        // Act
        var result = await _mediator.Send(new DeleteOrderCommand(order!.Id + 1));
        
        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task Should_ThrowError_When_UpdatingNonExistingOrder()
    {
        // Arrange
        var order = _provider
            .GetRequiredService<OrderDbContext>()
            .Orders
            .AsNoTracking()
            .OrderByDescending(x => x.Id).FirstOrDefault();

        var newOrder = new Order
        {
            Id = order!.Id + 1,
            Customer = "test",
            OrderNumber = "23232323"
        };

        // Act
        async Task UpdateOrder() => await _mediator.Send(new UpdateOrderCommand(newOrder));

        // Assert
        await Assert.ThrowsAsync<InvalidOperationException>(UpdateOrder);
    }

    public Task InitializeAsync() => Task.CompletedTask;
    public Task DisposeAsync() => _resetDatabase();
}