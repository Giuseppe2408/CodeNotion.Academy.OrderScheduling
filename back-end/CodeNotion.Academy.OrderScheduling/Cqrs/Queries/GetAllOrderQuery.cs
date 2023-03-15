using CodeNotion.Academy.OrderScheduling.Data;
using CodeNotion.Academy.OrderScheduling.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CodeNotion.Academy.OrderScheduling.Cqrs.Queries;

public record GetAllOrderQuery(string? Customer, string? OrderNumber) : IRequest<List<Order>>;

internal class GetAllOrdersHandler : IRequestHandler<GetAllOrderQuery, List<Order>>
{
    private readonly OrderDbContext _db;

    public GetAllOrdersHandler(OrderDbContext db)
    {
        _db = db;
    }

    public async Task<List<Order>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
    {
        if (request.Customer != null)
            return new List<Order>(_db.Orders.ToList().Where(or => or.Customer == request.Customer));
        if (request.OrderNumber != null)
            return new List<Order>(_db.Orders.ToList().Where(or => or.OrderNumber == request.OrderNumber));
        
        
        var orders = await _db.Orders.ToListAsync(cancellationToken);
        return orders;
    }
}