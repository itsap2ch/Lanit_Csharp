using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Models;

public class ExpenseItem
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public int CategoryId { get; set; }

    public Category? Category { get; set; }

    public ICollection<Transaction> Transactions { get; set; } = [];
}
