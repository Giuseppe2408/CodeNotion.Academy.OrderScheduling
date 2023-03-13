using CodeNotion.Academy.OrderScheduling.Data;
using CodeNotion.Academy.OrderScheduling.Models;
using MediatR;

namespace CodeNotion.Academy.OrderScheduling.Cqrs.Commands;

public record DeleteOrderCommand(int Id) : IRequest<Order>;

internal class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, Order>
{
    private readonly OrderDbContext _db;

    public DeleteOrderHandler(OrderDbContext db)
    {
        _db = db;
    }

    public Task<Order> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = GetById(request.Id);
        _db.Orders.Remove(order ?? throw new InvalidOperationException());
        _db.SaveChanges();

        return Task.FromResult(order);
    }

    private Order? GetById(int id) => _db.Orders.FirstOrDefault(or => or.Id == id);
}