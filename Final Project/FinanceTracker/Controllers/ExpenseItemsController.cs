using FinanceTracker.Data;
using FinanceTracker.DTOs.ExpenseItems;
using FinanceTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpenseItemsController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;

    /// <summary>
    /// Получить все статьи расходов
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExpenseItemDto>>> GetAll()
    {
        var items = await _context.ExpenseItems
            .Include(x => x.Category)
            .Select(x => new ExpenseItemDto
            {
                Id = x.Id,
                Name = x.Name,
                IsActive = x.IsActive,
                CategoryId = x.CategoryId,
                CategoryName = x.Category!.Name
            })
            .ToListAsync();

        return Ok(items);
    }

    /// <summary>
    /// Получить статью по Id
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ExpenseItemDto>> Get(int id)
    {
        var item = await _context.ExpenseItems
            .Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (item == null)
            return NotFound();

        return Ok(new ExpenseItemDto
        {
            Id = item.Id,
            Name = item.Name,
            IsActive = item.IsActive,
            CategoryId = item.CategoryId,
            CategoryName = item.Category!.Name
        });
    }

    /// <summary>
    /// Создать статью расходов
    /// </summary>
    [HttpPost]
    public async Task<ActionResult> Create(CreateExpenseItemDto dto)
    {
        var categoryExists = await _context.Categories
            .AnyAsync(c => c.Id == dto.CategoryId);

        if (!categoryExists)
            return BadRequest("Категория не существует.");

        var item = new ExpenseItem
        {
            Name = dto.Name,
            CategoryId = dto.CategoryId,
            IsActive = dto.IsActive
        };

        _context.ExpenseItems.Add(item);

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
    }

    /// <summary>
    /// Изменить статью расходов
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateExpenseItemDto dto)
    {
        var item = await _context.ExpenseItems.FindAsync(id);

        if (item == null)
            return NotFound();

        var categoryExists = await _context.Categories
            .AnyAsync(c => c.Id == dto.CategoryId);

        if (!categoryExists)
            return BadRequest("Категория не существует.");

        item.Name = dto.Name;
        item.CategoryId = dto.CategoryId;
        item.IsActive = dto.IsActive;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Удалить статью расходов
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.ExpenseItems.FindAsync(id);

        if (item == null)
            return NotFound();

        _context.ExpenseItems.Remove(item);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}
