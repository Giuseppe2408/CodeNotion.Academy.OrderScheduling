﻿using CodeNotion.Academy.OrderScheduling.Data;
using CodeNotion.Academy.OrderScheduling.Models;
using MediatR;

namespace CodeNotion.Academy.OrderScheduling.Commands;

public record DeleteOrderCommand(int Id) : IRequest<Order>;

internal class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, Order>
{
    private readonly OrderDbContext _db;
    private readonly Timer _sw;

    public DeleteOrderHandler(OrderDbContext db, Timer sw)
    {
        _db = db;
        _sw = sw;
    }

    public Task<Order> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        _sw.StartTimer();
        var order = GetById(request.Id);
        _db.Orders.Remove(order ?? throw new InvalidOperationException());
        _db.SaveChanges();
        _sw.EndTimer();
        return Task.FromResult(order);
    }

    private Order? GetById(int id) => _db.Orders.FirstOrDefault(or => or.Id == id);
}