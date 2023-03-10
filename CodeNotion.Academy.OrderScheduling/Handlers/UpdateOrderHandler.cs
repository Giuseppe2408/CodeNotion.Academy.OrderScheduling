using CodeNotion.Academy.OrderScheduling.Commands;
using CodeNotion.Academy.OrderScheduling.Models;
using MediatR;

namespace CodeNotion.Academy.OrderScheduling.Handlers;

public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, Order>
{
    private readonly IOrderRepository _orderRepository;

    public UpdateOrderHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    
    public Task<Order> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_orderRepository.Update(_orderRepository.GetById(request.Id) ?? throw new InvalidOperationException(), request.Order));
    }
}