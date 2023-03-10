using CodeNotion.Academy.OrderScheduling.Data;
using CodeNotion.Academy.OrderScheduling.Models;
using MediatR;

namespace CodeNotion.Academy.OrderScheduling.Commands;

public record CreateOrderCommand(Order Order) : IRequest<Order>;

internal class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Order>
{
    private readonly OrderDbContext _db;
    
    private readonly Timer _sw;

    public CreateOrderCommandHandler(OrderDbContext db, Timer sw)
    {
        _db = db;
        _sw = sw;
    }

    public Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        _sw.StartTimer();
        _db.Add(request.Order);
        _db.SaveChanges();
        _sw.EndTimer();
        return Task.FromResult(request.Order);
    }
}
