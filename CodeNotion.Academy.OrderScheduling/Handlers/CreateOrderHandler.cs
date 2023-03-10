using CodeNotion.Academy.OrderScheduling.Commands;
using CodeNotion.Academy.OrderScheduling.Models;
using MediatR;

namespace CodeNotion.Academy.OrderScheduling.Handlers;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Order>
{
    private readonly IOrderRepository _orderRepository;

    public CreateOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        
        return Task.FromResult(_orderRepository.Create(request.Order));
    }
}