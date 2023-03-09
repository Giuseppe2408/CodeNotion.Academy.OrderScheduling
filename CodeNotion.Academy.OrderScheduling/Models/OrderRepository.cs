using System.Diagnostics;
using CodeNotion.Academy.OrderScheduling.Data;

namespace CodeNotion.Academy.OrderScheduling.Models;

//repository per codice pulito
public class OrderRepository : IOrderRepository
{
    private readonly OrderDbContext _db;
    private readonly Stopwatch _sw = new();

    public OrderRepository(OrderDbContext db)
    {
        _db = db;
    }

    public List<Order> All()
    {
        StartTimer();
        var orders = _db.Orders.ToList();
        EndTimer();
        return orders;
    }

    public Order? GetById(int id) => _db.Orders.FirstOrDefault(or => or.Id == id);

    public void Create(Order order)
    {
        StartTimer();
        _db.Orders.Add(order);
        _db.SaveChanges();
        EndTimer();
    }

    public void Update(Order order, Order data)
    {
        StartTimer();
        order.Customer = data.Customer;
        order.OrderNumber = data.OrderNumber;
        order.CuttingDate = data.CuttingDate;
        order.PreparationDate = data.PreparationDate;
        order.BendingDate = data.BendingDate;
        order.AssemblyDate = data.AssemblyDate;

        _db.SaveChanges();
        EndTimer();
    }

    public void Delete(Order order)
    {
        StartTimer();
        _db.Remove(order);
        _db.SaveChanges();
        EndTimer();
    }

    private void StartTimer()
    {
        _sw.Start();
    }

    private void EndTimer()
    {
        _sw.Stop();
        Console.WriteLine("Tempo passato {0}", _sw.ElapsedMilliseconds);
    }
}