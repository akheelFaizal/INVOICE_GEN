using System;
using InvoiceSystem.Expenses.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvoiceSystem.Expenses.Infrastructure.Data;

public class ExpensesDbContext : DbContext
{
    public ExpensesDbContext(DbContextOptions<ExpensesDbContext> options) : base(options)
    {
    }

    public DbSet<Expense> Expenses { get; set; }
    public DbSet<ExpenseCategory> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");
            entity.HasOne(e => e.Category).WithMany().HasForeignKey(e => e.CategoryId);
        });

        modelBuilder.Entity<ExpenseCategory>(entity =>
        {
            entity.HasKey(e => e.Id);
        });
    }
}
