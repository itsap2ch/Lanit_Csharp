namespace FinanceTracker.DTOs.Categories;

public class CategoryDto
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public decimal MonthlyBudget { get; set; }

    public bool IsActive { get; set; }
}