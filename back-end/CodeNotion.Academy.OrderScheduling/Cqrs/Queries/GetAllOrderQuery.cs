using CodeNotion.Academy.OrderScheduling.Data;
using CodeNotion.Academy.OrderScheduling.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CodeNotion.Academy.OrderScheduling.Cqrs.Queries;

public record GetAllOrderQuery : IRequest<List<Order>>;

internal class GetAllOrdersHandler : IRequestHandler<GetAllOrderQuery, List<Order>>
{
    private readonly OrderDbContext _db;

    public GetAllOrdersHandler(OrderDbContext db)
    {
        _db = db;
    }

    public async Task<List<Order>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
    {
        var orders = await _db.Orders.ToListAsync(cancellationToken);
        return orders;
    }
}