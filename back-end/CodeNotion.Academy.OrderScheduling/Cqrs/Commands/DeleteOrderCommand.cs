using CodeNotion.Academy.OrderScheduling.Data;
using CodeNotion.Academy.OrderScheduling.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CodeNotion.Academy.OrderScheduling.Cqrs.Commands;

public record DeleteOrderCommand(int Id) : IRequest<Order>;

internal class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, Order>
{
    private readonly OrderDbContext _db;

    public DeleteOrderHandler(OrderDbContext db)
    {
        _db = db;
    }

    public async Task<Order> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await GetById(request.Id);

        if (order != null) _db.Orders.Remove(order);
        await _db.SaveChangesAsync(cancellationToken);
        return order!;
    }

    private Task<Order?> GetById(int id) => _db.Orders.FirstOrDefaultAsync(or => or.Id == id);
}