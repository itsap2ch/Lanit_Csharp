using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Models;

public class Category
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Range(0, 1_000_000)]
    public decimal MonthlyBudget { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<ExpenseItem> ExpenseItems { get; set; } = [];
}
