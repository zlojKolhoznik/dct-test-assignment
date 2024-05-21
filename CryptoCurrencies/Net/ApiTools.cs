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
        EnsureDeserialization(response);
        return response!.Data.Take(10);
    }
    
    /// <summary>
    /// Gets the currency with the specified ID.
    /// </summary>
    /// <param name="id">ID of the currency on the API</param>
    /// <returns>Currency object if such currency is found, otherwise null</returns>
    public static Currency? GetCurrency(string id)
    {
        var url = $"{_baseUrl}/assets/{id}";
        var json = GetResponse(url);
        var response = JsonSerializer.Deserialize<ApiResponse<Currency?>>(json);        
        EnsureDeserialization(response);
        return response!.Data;
    }

    public static IEnumerable<Market> GetMarkets(string currencyId)
    {
        var url = $"{_baseUrl}/assets/{currencyId}/markets";
        var json = GetResponse(url);
        var response = JsonSerializer.Deserialize<ApiResponse<IEnumerable<Market>>>(json);
        EnsureDeserialization(response);
        return response!.Data;
    }

    private static void EnsureDeserialization<T>(ApiResponse<T>? response)
    {
        if (response is null)
        {
            throw new SerializationException("Failed to deserialize response");
        }
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