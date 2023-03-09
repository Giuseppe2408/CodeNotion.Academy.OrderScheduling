namespace CodeNotion.Academy.OrderScheduling.Models;

/// <summary>
/// interfaccia per dependencies
/// </summary>
public interface IOrderRepository
{
    public List<Order> All();
    public void Create(Order order);
    public void Update(Order order, Order data);
    public Order? GetById(int id);
    public void Delete(Order order);
}