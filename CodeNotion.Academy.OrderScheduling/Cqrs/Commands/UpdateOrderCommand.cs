using CodeNotion.Academy.OrderScheduling.Data;
using CodeNotion.Academy.OrderScheduling.Models;
using MediatR;

namespace CodeNotion.Academy.OrderScheduling.Cqrs.Commands;

public record UpdateOrderCommand(int Id, Order Order) : IRequest<Order>;

internal class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, Order>
{
    private readonly OrderDbContext _db;

    public UpdateOrderHandler(OrderDbContext db)
    {
        _db = db;
    }

    public Task<Order> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = GetById(request.Id);
        if (order != null)
        {
            order.Customer = request.Order.Customer;
            order.OrderNumber = request.Order.OrderNumber;
            order.CuttingDate = request.Order.CuttingDate;
            order.PreparationDate = request.Order.PreparationDate;
            order.BendingDate = request.Order.BendingDate;
            order.AssemblyDate = request.Order.AssemblyDate;
        }

        _db.SaveChanges();
        return Task.FromResult(order ?? throw new InvalidOperationException());
    }

    private Order? GetById(int id) => _db.Orders.FirstOrDefault(or => or.Id == id);
}