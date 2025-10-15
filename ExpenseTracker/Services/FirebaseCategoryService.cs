using ExpenseTracker.Models;
using ExpenseTracker.Data;

namespace ExpenseTracker.Services;
using System.Net.Http.Json;

public class FirebaseCategoryService
{
    private readonly HttpClient _http;
    private readonly string _baseUrl = "https://firestore.googleapis.com/v1/projects/expensetracker-33faa/databases/(default)/documents/categories";

    public FirebaseCategoryService(HttpClient http)
    {
        _http = http;
    }

    public  async Task<List<Category>> GetCategoriesAsync()
    {
        var response = await _http.GetFromJsonAsync<FirestoreResponse>(_baseUrl);
        return response?.Documents?.Select(d => new Category
        {
            Name = d.Fields.name.StringValue ?? "Untitled",
            CreatedAt = DateTime.Parse(d.Fields.createdAt.TimestampValue ?? DateTime.Now.ToString())
        }).ToList() ?? new List<Category>();
    }

    public async Task AddCategoryAsync(Category Category)
    {
        var doc = new
        {
            fields = new
            {
                name = new { stringValue = Category.Name },
                createdAt = new { timestampValue = Category.CreatedAt.ToString("o") }
            }
        };
        await _http.PostAsJsonAsync(_baseUrl, doc);
    }

   //Models for Firestore response
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
       public FirestoreValue name { get; set; } = new();
       public FirestoreValue createdAt { get; set; } = new();
   }
   
   public class FirestoreValue
   {
       public string? StringValue { get; set; }
       public string? TimestampValue { get; set; }
   }
}
