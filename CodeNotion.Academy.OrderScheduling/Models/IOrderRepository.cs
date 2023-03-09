namespace CodeNotion.Academy.OrderScheduling.Models;

//interfaccia per dependencies
public interface IOrderRepository
{
    public List<Order> All();
    
    public void Create(Order order);

    public void Update(Order order, Order data);
    
    public Order GetById(int id);
    
    public void Delete(Order order);

    
}