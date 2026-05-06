using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InvoiceSystem.Expenses.Core.Entities;

namespace InvoiceSystem.Expenses.Core.Interfaces;

public interface IExpenseRepository
{
    Task<IEnumerable<Expense>> GetAllAsync();
    Task<Expense?> GetByIdAsync(Guid id);
    Task AddAsync(Expense expense);
    Task UpdateAsync(Expense expense);
    Task DeleteAsync(Expense expense);
}

public interface IExpenseCategoryRepository
{
    Task<IEnumerable<ExpenseCategory>> GetAllAsync();
    Task<ExpenseCategory?> GetByIdAsync(Guid id);
    Task AddAsync(ExpenseCategory category);
    Task UpdateAsync(ExpenseCategory category);
    Task DeleteAsync(ExpenseCategory category);
}
