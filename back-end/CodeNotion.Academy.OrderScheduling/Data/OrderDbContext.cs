﻿using CodeNotion.Academy.OrderScheduling.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CodeNotion.Academy.OrderScheduling.Data;

public class OrderDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; } = null!;

    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = WebApplication.CreateBuilder();
        var connectionString = builder.Configuration.GetConnectionString("MyConnectionString");
        optionsBuilder.UseSqlServer(
            connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Order>()
            .Property(x => x.CuttingDate)
            .HasConversion<DateOnlyConverter>()
            .HasColumnType("date");

        modelBuilder.Entity<Order>()
            .Property(x => x.AssemblyDate)
            .HasConversion<DateOnlyConverter>()
            .HasColumnType("date");

        modelBuilder.Entity<Order>()
            .Property(x => x.BendingDate)
            .HasConversion<DateOnlyConverter>()
            .HasColumnType("date");

        modelBuilder.Entity<Order>()
            .Property(x => x.PreparationDate)
            .HasConversion<DateOnlyConverter>()
            .HasColumnType("date");
    }
}

/// <summary>
/// Converts <see cref="DateOnly" /> to <see cref="DateTime"/> and vice versa.
/// </summary>
public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
{
    /// <summary>
    /// Creates a new instance of this converter.
    /// </summary>
    public DateOnlyConverter() : base(
        d => d.ToDateTime(TimeOnly.MinValue),
        d => DateOnly.FromDateTime(d))
    {
    }
}