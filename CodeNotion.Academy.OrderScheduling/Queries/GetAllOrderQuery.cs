using CodeNotion.Academy.OrderScheduling.Data;
using CodeNotion.Academy.OrderScheduling.Models;
using MediatR;

namespace CodeNotion.Academy.OrderScheduling.Queries;

public class GetAllOrderQuery : IRequest<List<Order>> {}

internal class GetAllOrdersHandler : IRequestHandler<GetAllOrderQuery, List<Order>>
{
    private readonly OrderDbContext _db;
    private readonly Timer _sw;
    
    public GetAllOrdersHandler(OrderDbContext db, Timer sw)
    {
        _db = db;
        _sw = sw;
    }
    
    public Task<List<Order>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
    {
        _sw.StartTimer();
        var orders = _db.Orders.ToList();
        _sw.EndTimer();
        return Task.FromResult(orders);
    }
}
