using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvoiceSystem.Expenses.Core.Entities;
using InvoiceSystem.Expenses.Core.Interfaces;
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

namespace InvoiceSystem.Expenses.Infrastructure.Repositories;

public class ExpenseRepository : IExpenseRepository
{
    private readonly ExpensesDbContext _context;

    public ExpenseRepository(ExpensesDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Expense>> GetAllAsync()
    {
        return await _context.Expenses.Include(e => e.Category).ToListAsync();
    }

    public async Task<Expense?> GetByIdAsync(Guid id)
    {
        return await _context.Expenses.Include(e => e.Category).FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task AddAsync(Expense expense)
    {
        await _context.Expenses.AddAsync(expense);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Expense expense)
    {
        _context.Expenses.Update(expense);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Expense expense)
    {
        _context.Expenses.Remove(expense);
        await _context.SaveChangesAsync();
    }
}

public class ExpenseCategoryRepository : IExpenseCategoryRepository
{
    private readonly ExpensesDbContext _context;

    public ExpenseCategoryRepository(ExpensesDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ExpenseCategory>> GetAllAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<ExpenseCategory?> GetByIdAsync(Guid id)
    {
        return await _context.Categories.FindAsync(id);
    }

    public async Task AddAsync(ExpenseCategory category)
    {
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ExpenseCategory category)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(ExpenseCategory category)
    {
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }
}
