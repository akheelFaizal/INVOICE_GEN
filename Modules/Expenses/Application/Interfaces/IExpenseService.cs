using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InvoiceSystem.Expenses.Application.DTOs;
using InvoiceSystem.Shared;

namespace InvoiceSystem.Expenses.Application.Interfaces;

public interface IExpenseService
{
    Task<Result<IEnumerable<ExpenseResponse>>> GetAllExpensesAsync();
    Task<Result<ExpenseResponse>> GetExpenseByIdAsync(Guid id);
    Task<Result<ExpenseResponse>> CreateExpenseAsync(CreateExpenseRequest request);
    Task<Result<ExpenseResponse>> UpdateExpenseAsync(Guid id, UpdateExpenseRequest request);
    Task<Result> DeleteExpenseAsync(Guid id);
}

public interface IExpenseCategoryService
{
    Task<Result<IEnumerable<ExpenseCategoryResponse>>> GetAllCategoriesAsync();
    Task<Result<ExpenseCategoryResponse>> GetCategoryByIdAsync(Guid id);
    Task<Result<ExpenseCategoryResponse>> CreateCategoryAsync(CreateExpenseCategoryRequest request);
    Task<Result<ExpenseCategoryResponse>> UpdateCategoryAsync(Guid id, UpdateExpenseCategoryRequest request);
    Task<Result> DeleteCategoryAsync(Guid id);
}
