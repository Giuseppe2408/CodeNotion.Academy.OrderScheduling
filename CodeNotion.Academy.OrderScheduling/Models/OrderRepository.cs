using System.Diagnostics;
using CodeNotion.Academy.OrderScheduling.Data;
using Microsoft.EntityFrameworkCore;

namespace CodeNotion.Academy.OrderScheduling.Models;
//repository per codice pulito
public class OrderRepository : IOrderRepository
{
    private OrderDbContext db;
    private Stopwatch sw = new Stopwatch();

    public OrderRepository(OrderDbContext _db)
    {
        db = _db;
    }
    
    public List<Order> All()
    {
        StartTimer();
            List<Order> orders = db.Orders.ToList();
        EndTimer();
        return orders;
        
    }
    
    public Order GetById(int id)
    {
        return db.Orders.Where(or => or.Id == id).FirstOrDefault();
        
    }

    public void Create(Order order)
    {
        StartTimer();
            db.Orders.Add(order);
            db.SaveChanges();
        EndTimer();
    }

    public void Update(Order order, Order data)
    {
        StartTimer();
            order.Customer = data.Customer;
            order.Order_number = data.Order_number;
            order.Cutting_date = data.Cutting_date;
            order.Preparation_date = data.Preparation_date;
            order.Bending_date = data.Bending_date;
            order.Assembly_date = data.Assembly_date;
        
            db.SaveChanges();
        EndTimer();    
    }
    

    public void Delete(Order order)
    {
        StartTimer();
            db.Remove(order);
            db.SaveChanges();
        EndTimer();
        
    }
    //faccio partire il timer 
    public void StartTimer()
    {
        
        sw.Start(); //parte il timer
        
    }
    //stoppo timer e ottengo risposta in millisecondi
    public void EndTimer()
    {
        
        sw.Stop(); //parte il timer
        Console.WriteLine("Tempo passato {0}", sw.ElapsedMilliseconds);
        
    }
}