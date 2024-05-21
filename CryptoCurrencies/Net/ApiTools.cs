using System.Net.Http;
using System.Runtime.Serialization;
using System.Text.Json;
using CryptoCurrencies.Models;

namespace CryptoCurrencies.Net;

public static class ApiTools
{
    private static string _baseUrl = "https://api.coincap.io/v2";
    
    /// <summary>
    /// Gets the top 10 currencies returned by the API.
    /// </summary>
    /// <returns>List of top 10 currencies</returns>
    /// <exception cref="SerializationException">Failed to deserialize response</exception>
    /// <exception cref="HttpRequestException">The HTTP response is unsucsessfull</exception>
    public static IEnumerable<Currency> GetTop10Currencies()
    {
        var url = $"{_baseUrl}/assets";
        var json = GetResponse(url);
        var response = JsonSerializer.Deserialize<ApiResponse<IEnumerable<Currency>>>(json);
        if (response is null)
        {
            throw new SerializationException("Failed to deserialize response");
        }
        return response.Data.Take(10);
    }
    
    /// <summary>
    /// Sends a GET request to the specified URL and returns the response as a string.
    /// </summary>
    /// <param name="url">URL of GET request</param>
    /// <returns>Body of the request</returns>
    /// <exception cref="HttpRequestException">The HTTP response is unsucsessfull</exception>
    private static string GetResponse(string url)
    {
        using var client = new HttpClient();
        var response = client.GetAsync(url).Result;
        response.EnsureSuccessStatusCode();
        return response.Content.ReadAsStringAsync().Result;
    }
}