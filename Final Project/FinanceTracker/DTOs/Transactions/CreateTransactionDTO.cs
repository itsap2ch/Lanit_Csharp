using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.DTOs.Transactions;

public class CreateTransactionDto
{
    [Required]
    public DateOnly Date { get; set; }

    [Range(0.01, 1_000_000)]
    public decimal Amount { get; set; }

    [MaxLength(255)]
    public string? Comment { get; set; }

    [Required]
    public int ExpenseItemId { get; set; }
}