using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Models;

public class Expense
{
    [Required]
    public string Title { get; set; } = "";

    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
    public decimal Amount { get; set; }

    [Required]
    public string Category { get; set; } = "";

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
