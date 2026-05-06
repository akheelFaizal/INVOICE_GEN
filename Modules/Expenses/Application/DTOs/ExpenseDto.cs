using System;
using System.Collections.Generic;

namespace InvoiceSystem.Expenses.Application.DTOs;

// Expenses
public record CreateExpenseRequest(string Description, decimal Amount, DateTime Date, Guid CategoryId, string? ReceiptFileId);
public record UpdateExpenseRequest(string Description, decimal Amount, DateTime Date, Guid CategoryId, string? ReceiptFileId);
public record ExpenseResponse(Guid Id, string Description, decimal Amount, DateTime Date, Guid CategoryId, string CategoryName, string? ReceiptFileId);

// Categories
public record CreateExpenseCategoryRequest(string Name);
public record UpdateExpenseCategoryRequest(string Name);
public record ExpenseCategoryResponse(Guid Id, string Name);
