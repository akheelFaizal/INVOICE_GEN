using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvoiceSystem.Expenses.Application.DTOs;
using InvoiceSystem.Expenses.Application.Interfaces;
using InvoiceSystem.Expenses.Core.Entities;
using InvoiceSystem.Expenses.Core.Interfaces;
using InvoiceSystem.Shared;

namespace InvoiceSystem.Expenses.Application.Services;

public class ExpenseService : IExpenseService
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IExpenseCategoryRepository _categoryRepository;

    public ExpenseService(IExpenseRepository expenseRepository, IExpenseCategoryRepository categoryRepository)
    {
        _expenseRepository = expenseRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<Result<IEnumerable<ExpenseResponse>>> GetAllExpensesAsync()
    {
        var expenses = await _expenseRepository.GetAllAsync();
        var response = expenses.Select(MapToResponse);
        return Result<IEnumerable<ExpenseResponse>>.SuccessResult(response);
    }

    public async Task<Result<ExpenseResponse>> GetExpenseByIdAsync(Guid id)
    {
        var expense = await _expenseRepository.GetByIdAsync(id);
        if (expense == null) return Result<ExpenseResponse>.FailureResult("Expense not found");
        return Result<ExpenseResponse>.SuccessResult(MapToResponse(expense));
    }

    public async Task<Result<ExpenseResponse>> CreateExpenseAsync(CreateExpenseRequest request)
    {
        var expense = new Expense
        {
            Id = Guid.NewGuid(),
            Description = request.Description,
            Amount = request.Amount,
            Date = request.Date,
            CategoryId = request.CategoryId,
            ReceiptFileId = request.ReceiptFileId
        };

        await _expenseRepository.AddAsync(expense);
        
        // We need category name for response, fetch it
        var category = await _categoryRepository.GetByIdAsync(expense.CategoryId);
        expense.Category = category!;

        return Result<ExpenseResponse>.SuccessResult(MapToResponse(expense));
    }

    public async Task<Result<ExpenseResponse>> UpdateExpenseAsync(Guid id, UpdateExpenseRequest request)
    {
        var expense = await _expenseRepository.GetByIdAsync(id);
        if (expense == null) return Result<ExpenseResponse>.FailureResult("Expense not found");

        expense.Description = request.Description;
        expense.Amount = request.Amount;
        expense.Date = request.Date;
        expense.CategoryId = request.CategoryId;
        expense.ReceiptFileId = request.ReceiptFileId;

        await _expenseRepository.UpdateAsync(expense);
        
        var category = await _categoryRepository.GetByIdAsync(expense.CategoryId);
        expense.Category = category!;

        return Result<ExpenseResponse>.SuccessResult(MapToResponse(expense));
    }

    public async Task<Result> DeleteExpenseAsync(Guid id)
    {
        var expense = await _expenseRepository.GetByIdAsync(id);
        if (expense == null) return Result.FailureResult("Expense not found");

        await _expenseRepository.DeleteAsync(expense);
        return Result.SuccessResult();
    }

    private static ExpenseResponse MapToResponse(Expense expense)
    {
        return new ExpenseResponse(
            expense.Id,
            expense.Description,
            expense.Amount,
            expense.Date,
            expense.CategoryId,
            expense.Category?.Name ?? "Unknown",
            expense.ReceiptFileId
        );
    }
}

public class ExpenseCategoryService : IExpenseCategoryService
{
    private readonly IExpenseCategoryRepository _categoryRepository;

    public ExpenseCategoryService(IExpenseCategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Result<IEnumerable<ExpenseCategoryResponse>>> GetAllCategoriesAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        var response = categories.Select(c => new ExpenseCategoryResponse(c.Id, c.Name));
        return Result<IEnumerable<ExpenseCategoryResponse>>.SuccessResult(response);
    }

    public async Task<Result<ExpenseCategoryResponse>> GetCategoryByIdAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null) return Result<ExpenseCategoryResponse>.FailureResult("Category not found");
        return Result<ExpenseCategoryResponse>.SuccessResult(new ExpenseCategoryResponse(category.Id, category.Name));
    }

    public async Task<Result<ExpenseCategoryResponse>> CreateCategoryAsync(CreateExpenseCategoryRequest request)
    {
        var category = new ExpenseCategory { Id = Guid.NewGuid(), Name = request.Name };
        await _categoryRepository.AddAsync(category);
        return Result<ExpenseCategoryResponse>.SuccessResult(new ExpenseCategoryResponse(category.Id, category.Name));
    }

    public async Task<Result<ExpenseCategoryResponse>> UpdateCategoryAsync(Guid id, UpdateExpenseCategoryRequest request)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null) return Result<ExpenseCategoryResponse>.FailureResult("Category not found");

        category.Name = request.Name;
        await _categoryRepository.UpdateAsync(category);
        return Result<ExpenseCategoryResponse>.SuccessResult(new ExpenseCategoryResponse(category.Id, category.Name));
    }

    public async Task<Result> DeleteCategoryAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null) return Result.FailureResult("Category not found");

        await _categoryRepository.DeleteAsync(category);
        return Result.SuccessResult();
    }
}
