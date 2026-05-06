using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using InvoiceSystem.Expenses.Application.Interfaces;
using InvoiceSystem.Expenses.Application.DTOs;
using InvoiceSystem.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceSystem.Expenses.API.Controllers;

[Authorize(Roles = "Admin,Accountant")]
[ApiController]
[Route("api/[controller]")]
public class ExpensesController : ControllerBase
{
    private readonly IExpenseService _expenseService;

    public ExpensesController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    [HttpGet]
    public async Task<ActionResult<Result<IEnumerable<ExpenseResponse>>>> GetAll()
    {
        var result = await _expenseService.GetAllExpensesAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Result<ExpenseResponse>>> GetById(Guid id)
    {
        var result = await _expenseService.GetExpenseByIdAsync(id);
        if (!result.Success) return NotFound(result);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Result<ExpenseResponse>>> Create(CreateExpenseRequest request)
    {
        var result = await _expenseService.CreateExpenseAsync(request);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Result<ExpenseResponse>>> Update(Guid id, UpdateExpenseRequest request)
    {
        var result = await _expenseService.UpdateExpenseAsync(id, request);
        if (!result.Success) return NotFound(result);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Result>> Delete(Guid id)
    {
        var result = await _expenseService.DeleteExpenseAsync(id);
        if (!result.Success) return NotFound(result);
        return Ok(result);
    }
}

[Authorize(Roles = "Admin,Accountant")]
[ApiController]
[Route("api/expense-categories")]
public class ExpenseCategoriesController : ControllerBase
{
    private readonly IExpenseCategoryService _categoryService;

    public ExpenseCategoriesController(IExpenseCategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<Result<IEnumerable<ExpenseCategoryResponse>>>> GetAll()
    {
        var result = await _categoryService.GetAllCategoriesAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Result<ExpenseCategoryResponse>>> GetById(Guid id)
    {
        var result = await _categoryService.GetCategoryByIdAsync(id);
        if (!result.Success) return NotFound(result);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Result<ExpenseCategoryResponse>>> Create(CreateExpenseCategoryRequest request)
    {
        var result = await _categoryService.CreateCategoryAsync(request);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Result<ExpenseCategoryResponse>>> Update(Guid id, UpdateExpenseCategoryRequest request)
    {
        var result = await _categoryService.UpdateCategoryAsync(id, request);
        if (!result.Success) return NotFound(result);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Result>> Delete(Guid id)
    {
        var result = await _categoryService.DeleteCategoryAsync(id);
        if (!result.Success) return NotFound(result);
        return Ok(result);
    }
}
