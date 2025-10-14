namespace ExpenseTracker.Models;

public class Category
{
    public string Name {get; set;} = "";
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}