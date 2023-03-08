using CodeNotion.Academy.OrderScheduling.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeNotion.Academy.OrderScheduling.Data;

public class OrderDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }


    //strina di connessione
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("data source=a6584a657b52;initial catalog=master;user id=sa;password=yourStrong(!)Password;TrustServerCertificate=True;");
    }
}