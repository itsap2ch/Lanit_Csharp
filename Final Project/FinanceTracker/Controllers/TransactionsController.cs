using FinanceTracker.Data;
using FinanceTracker.DTOs.Transactions;
using FinanceTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;

    /// <summary>
    /// Все транзакции
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransactionDto>>> GetAll()
    {
        var result = await _context.Transactions
            .Include(t => t.ExpenseItem)
            .Select(t => new TransactionDto
            {
                Id = t.Id,
                Date = t.Date,
                Amount = t.Amount,
                Comment = t.Comment,
                ExpenseItemId = t.ExpenseItemId,
                ExpenseItemName = t.ExpenseItem!.Name
            })
            .ToListAsync();

        return Ok(result);
    }

    /// <summary>
    /// Транзакции за день
    /// </summary>
    [HttpGet("day")]
    public async Task<ActionResult<IEnumerable<TransactionDto>>> GetByDay(DateOnly date)
    {
        var result = await _context.Transactions
            .Include(t => t.ExpenseItem)
            .Where(t => t.Date == date)
            .Select(t => new TransactionDto
            {
                Id = t.Id,
                Date = t.Date,
                Amount = t.Amount,
                Comment = t.Comment,
                ExpenseItemId = t.ExpenseItemId,
                ExpenseItemName = t.ExpenseItem!.Name
            })
            .ToListAsync();

        return Ok(result);
    }

    /// <summary>
    /// Транзакции за месяц
    /// </summary>
    [HttpGet("month")]
    public async Task<ActionResult<IEnumerable<TransactionDto>>> GetByMonth(int year, int month)
    {
        var result = await _context.Transactions
            .Include(t => t.ExpenseItem)
            .Where(t => t.Date.Year == year && t.Date.Month == month)
            .Select(t => new TransactionDto
            {
                Id = t.Id,
                Date = t.Date,
                Amount = t.Amount,
                Comment = t.Comment,
                ExpenseItemId = t.ExpenseItemId,
                ExpenseItemName = t.ExpenseItem!.Name
            })
            .ToListAsync();

        return Ok(result);
    }

    /// <summary>
    /// Создать транзакцию (ГЛАВНАЯ ЛОГИКА)
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(CreateTransactionDto dto)
    {
        // 1. Проверка активной статьи
        var expenseItem = await _context.ExpenseItems
            .FirstOrDefaultAsync(x => x.Id == dto.ExpenseItemId);

        if (expenseItem == null)
            return BadRequest("Статья расходов не найдена.");

        if (!expenseItem.IsActive)
            return BadRequest("Статья расходов неактивна.");

        // 2. Лимит 1 000 000 в день
        var dayTotal = await _context.Transactions
            .Where(t => t.Date == dto.Date)
            .SumAsync(t => t.Amount);

        if (dayTotal + dto.Amount > 1_000_000)
            return BadRequest("Превышен дневной лимит 1 000 000 ₽.");

        // 3. Создание транзакции
        var transaction = new Transaction
        {
            Date = dto.Date,
            Amount = dto.Amount,
            Comment = dto.Comment,
            ExpenseItemId = dto.ExpenseItemId
        };

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        return Ok(transaction);
    }

    /// <summary>
    /// Обновление транзакции
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateTransactionDto dto)
    {
        var transaction = await _context.Transactions
            .Include(t => t.ExpenseItem)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (transaction == null)
            return NotFound();

        // запрет смены статьи если она стала неактивной
        var expenseItem = await _context.ExpenseItems
            .FirstOrDefaultAsync(x => x.Id == dto.ExpenseItemId);

        if (expenseItem == null)
            return BadRequest("Статья не найдена.");

        if (!expenseItem.IsActive)
            return BadRequest("Нельзя выбрать неактивную статью.");

        transaction.Date = dto.Date;
        transaction.Amount = dto.Amount;
        transaction.Comment = dto.Comment;
        transaction.ExpenseItemId = dto.ExpenseItemId;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Удалить транзакцию
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var transaction = await _context.Transactions.FindAsync(id);

        if (transaction == null)
            return NotFound();

        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Стикер дня (цвет)
    /// </summary>
    [HttpGet("sticker")]
    public async Task<IActionResult> GetSticker(DateOnly date)
    {
        var sum = await _context.Transactions
            .Where(t => t.Date == date)
            .SumAsync(t => t.Amount);

        string color =
            sum < 500 ? "green" :
            sum <= 2000 ? "yellow" :
            "red";

        return Ok(new
        {
            date,
            sum,
            color
        });
    }
}