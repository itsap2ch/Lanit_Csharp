using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.DTOs.ExpenseItems;

public class CreateExpenseItemDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = "";

    [Required]
    public int CategoryId { get; set; }

    public bool IsActive { get; set; } = true;
}