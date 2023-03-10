namespace CodeNotion.Academy.OrderScheduling.Models;

/// <summary>
/// interfaccia per dependencies
/// </summary>
public interface IOrderRepository
{
    public List<Order> All();
    public Order Create(Order order);
    public void Update(Order order, Order data);
    public Order? GetById(int id);
    public Order Delete(Order order);
}