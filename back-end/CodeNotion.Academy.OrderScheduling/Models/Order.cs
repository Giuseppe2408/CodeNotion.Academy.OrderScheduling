using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;

namespace CodeNotion.Academy.OrderScheduling.Models;

public class Order
{
    public int Id { get; set; }
    public string Customer { get; set; } = null!;
    public string OrderNumber { get; set; } = null!;
    public DateOnly? CuttingDate { get; set; }
    public DateOnly? PreparationDate { get; set; }
    public DateOnly? BendingDate { get; set; }
    public DateOnly? AssemblyDate { get; set; }
}