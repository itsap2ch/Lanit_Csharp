using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.DTOs.Categories;

public class CreateCategoryDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = "";

    [Range(0, 1_000_000)]
    public decimal MonthlyBudget { get; set; }

    public bool IsActive { get; set; } = true;
}