using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Models;

public class Transaction
{
    public int Id { get; set; }

    [Required]
    public DateOnly Date { get; set; }

    [Range(0.01, 1_000_000)]
    public decimal Amount { get; set; }

    [MaxLength(255)]
    public string? Comment { get; set; }

    public int ExpenseItemId { get; set; }

    public ExpenseItem? ExpenseItem { get; set; }
}
