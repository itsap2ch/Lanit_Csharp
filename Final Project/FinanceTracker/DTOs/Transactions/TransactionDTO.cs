namespace FinanceTracker.DTOs.Transactions;

public class TransactionDto
{
    public int Id { get; set; }

    public DateOnly Date { get; set; }

    public decimal Amount { get; set; }

    public string? Comment { get; set; }

    public int ExpenseItemId { get; set; }

    public string ExpenseItemName { get; set; } = "";
}