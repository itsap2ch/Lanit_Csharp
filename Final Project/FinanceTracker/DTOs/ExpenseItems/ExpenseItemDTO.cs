namespace FinanceTracker.DTOs.ExpenseItems;

public class ExpenseItemDto
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public bool IsActive { get; set; }

    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = "";
}