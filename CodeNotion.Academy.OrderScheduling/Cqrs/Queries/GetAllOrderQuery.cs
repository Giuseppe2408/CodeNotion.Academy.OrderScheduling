using CodeNotion.Academy.OrderScheduling.Data;
using CodeNotion.Academy.OrderScheduling.Models;
using MediatR;

namespace CodeNotion.Academy.OrderScheduling.cqrs.Queries;

public class GetAllOrderQuery : IRequest<List<Order>> {}

internal class GetAllOrdersHandler : IRequestHandler<GetAllOrderQuery, List<Order>>
{
    private readonly OrderDbContext _db;

    public GetAllOrdersHandler(OrderDbContext db)
    {
        _db = db;
    }
    
    public Task<List<Order>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
    {
        
        var orders = _db.Orders.ToList();
        
        return Task.FromResult(orders);
    }
}