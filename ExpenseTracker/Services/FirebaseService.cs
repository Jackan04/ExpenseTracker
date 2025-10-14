using ExpenseTracker.Models;

namespace ExpenseTracker.Services;
using System.Net.Http.Json;

public class FirebaseService
{
    private readonly HttpClient _http;
    private readonly string _baseUrl = "https://firestore.googleapis.com/v1/projects/expensetracker-33faa/databases/(default)/documents/expenses";

    public FirebaseService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<Expense>> GetExpensesAsync()
    {
        var response = await _http.GetFromJsonAsync<FirestoreResponse>(_baseUrl);
        return response?.Documents?.Select(d => new Expense
        {
            Title = d.Fields.title.StringValue ?? "Untitled",
            Amount = decimal.Parse(d.Fields.amount.IntegerValue ?? "0"),
            Category = d.Fields.category.StringValue ?? "",
            CreatedAt = DateTime.Parse(d.Fields.createdAt.TimestampValue ?? DateTime.Now.ToString())
        }).ToList() ?? new List<Expense>();
    }

    public async Task AddExpenseAsync(Expense expense)
    {
        var doc = new
        {
            fields = new
            {
                title = new { stringValue = expense.Title },
                amount = new { integerValue = expense.Amount.ToString() },
                category = new { stringValue = expense.Category },
                createdAt = new { timestampValue = expense.CreatedAt.ToString("o") }
            }
        };
        await _http.PostAsJsonAsync(_baseUrl, doc);
    }

    // Models for Firestore response
    public class FirestoreResponse
    {
        public List<FirestoreDocument>? Documents { get; set; }
    }

    public class FirestoreDocument
    {
        public FirestoreFields Fields { get; set; } = new();
    }

    public class FirestoreFields
    {
        public FirestoreValue title { get; set; } = new();
        public FirestoreValue amount { get; set; } = new();
        public FirestoreValue category { get; set; } = new();
        public FirestoreValue createdAt { get; set; } = new();
    }

    public class FirestoreValue
    {
        public string? StringValue { get; set; }
        public string? IntegerValue { get; set; }
        public string? TimestampValue { get; set; }
    }
}
