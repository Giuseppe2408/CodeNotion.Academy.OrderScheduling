using CodeNotion.Academy.OrderScheduling.Models;
using CodeNotion.Academy.OrderScheduling.Queries;
using MediatR;

namespace CodeNotion.Academy.OrderScheduling.Handlers;

public class GetAllOrdersHandler : IRequestHandler<GetAllOrderQuery, List<Order>>
{
    private readonly IOrderRepository _orderRepository;
    
    public GetAllOrdersHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    
    public Task<List<Order>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
    {
        var orders = _orderRepository.All();
        return Task.FromResult(orders);
    }
}