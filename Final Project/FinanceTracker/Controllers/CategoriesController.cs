using FinanceTracker.Data;
using FinanceTracker.DTOs.Categories;
using FinanceTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;

    /// <summary>
    /// Получить все категории
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
    {
        var categories = await _context.Categories
            .Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                MonthlyBudget = c.MonthlyBudget,
                IsActive = c.IsActive
            })
            .ToListAsync();

        return Ok(categories);
    }

    /// <summary>
    /// Получить категорию по Id
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoryDto>> Get(int id)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category == null)
            return NotFound();

        return Ok(new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            MonthlyBudget = category.MonthlyBudget,
            IsActive = category.IsActive
        });
    }

    /// <summary>
    /// Создать категорию
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<CategoryDto>> Create(CreateCategoryDto dto)
    {
        var category = new Category
        {
            Name = dto.Name,
            MonthlyBudget = dto.MonthlyBudget,
            IsActive = dto.IsActive
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get),
            new { id = category.Id },
            new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                MonthlyBudget = category.MonthlyBudget,
                IsActive = category.IsActive
            });
    }

    /// <summary>
    /// Изменить категорию
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateCategoryDto dto)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category == null)
            return NotFound();

        category.Name = dto.Name;
        category.MonthlyBudget = dto.MonthlyBudget;
        category.IsActive = dto.IsActive;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Удалить категорию
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category == null)
            return NotFound();

        _context.Categories.Remove(category);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}