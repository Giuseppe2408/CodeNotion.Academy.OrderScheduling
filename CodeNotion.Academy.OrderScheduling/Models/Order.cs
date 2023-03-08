using System.Runtime.InteropServices.JavaScript;

namespace CodeNotion.Academy.OrderScheduling.Models;

public class Order
{
    public int Id { get; set; }
    
    public string Customer { get; set; }
    
    public string Order_number { get; set; }
    
    public DateTime Cutting_date { get; set; }
    
    public DateTime Preparation_date { get; set; }
    
    public DateTime Bending_date { get; set; }
    
    public DateTime Assembly_date { get; set; }
}