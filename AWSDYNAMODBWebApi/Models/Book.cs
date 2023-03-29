using Amazon.DynamoDBv2.DataModel;
using System.Text.Json.Serialization;

namespace AWSDYNAMODBWebApi.Models
{
    [DynamoDBTable("books")]
    public class Book
    {
        [DynamoDBHashKey("id"),JsonPropertyName("id")]
        public string? Id { get; set; }

        [DynamoDBProperty("name"), JsonPropertyName("name")]
        public string? Name { get; set; }

    }
}
