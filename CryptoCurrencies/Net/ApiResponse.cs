using System.Text.Json.Serialization;

namespace CryptoCurrencies.Net;

/// <summary>
/// Response from the API.
/// </summary>
/// <typeparam name="T">Type of the Data property</typeparam>
public class ApiResponse<T>
{
    [JsonPropertyName("data")]
    public T Data { get; set; }
    
    [JsonPropertyName("timestamp")]
    public long Timestamp { get; set; }
}