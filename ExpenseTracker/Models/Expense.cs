namespace ExpenseTracker.Models;

public class Expense
{
    public string Title { get; set; } = "";
    public decimal Amount { get; set; }
    public string Category { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
