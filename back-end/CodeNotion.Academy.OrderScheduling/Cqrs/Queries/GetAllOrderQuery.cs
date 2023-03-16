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
        var orders = _db.Orders;

        if (request.Customer != null)
            return await orders.Where(order => order.Customer.ToLower().Contains(request.Customer.ToLower()))
                .ToListAsync(cancellationToken: cancellationToken);

        if (request.OrderNumber != null)
            return await orders.Where(order => order.OrderNumber.ToLower().Contains(request.OrderNumber.ToLower()))
                .ToListAsync(cancellationToken: cancellationToken);

        return await orders.ToListAsync(cancellationToken);
    }
}