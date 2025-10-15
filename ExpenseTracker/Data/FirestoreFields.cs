namespace ExpenseTracker.Data;

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