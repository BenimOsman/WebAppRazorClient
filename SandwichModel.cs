    using System.Text.Json.Serialization;                           // Needed for JSON property

namespace WebAppRazorClient
{
    public record class SandwichModel(                              // Define a record class for SandwichModel
        [property: JsonPropertyName("id")] int Id,                  // Map C# property 'Id' to JSON field "id"
        [property: JsonPropertyName("name")] string Name,           // Map C# property 'Name' to JSON field "name"
        [property: JsonPropertyName("price")] double Price          // Map C# property 'Price' to JSON field "price"
    );
}

// Records: Record in C# is a reference type that provides a simplified syntax for defining immutable data models.