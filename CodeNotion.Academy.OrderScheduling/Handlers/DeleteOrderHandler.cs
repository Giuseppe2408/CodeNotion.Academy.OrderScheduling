using CodeNotion.Academy.OrderScheduling.Commands;
using CodeNotion.Academy.OrderScheduling.Models;
using MediatR;

namespace CodeNotion.Academy.OrderScheduling.Handlers;

public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, Order>
{
    private readonly IOrderRepository _orderRepository;

    public DeleteOrderHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public Task<Order> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_orderRepository.Delete(_orderRepository.GetById(request.Id) ?? throw new InvalidOperationException()));
    }
}