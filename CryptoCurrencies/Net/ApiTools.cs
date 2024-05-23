using System.Collections;
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
        return response!.Data.OrderBy(c => int.Parse(c.Rank)).Take(10);
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
    
    /// <summary>
    /// Gets the currency with the specified symbol.
    /// </summary>
    /// <param name="symbol">Symbol of the currency (e.g. BTC)</param>
    /// <returns>Currency object if such currency is found, otherwise null</returns>
    public static Currency? GetCurrencyBySymbol(string symbol)
    {
        var url = $"{_baseUrl}/assets";
        var json = GetResponse(url);
        var response = JsonSerializer.Deserialize<ApiResponse<IEnumerable<Currency>>>(json);
        EnsureDeserialization(response);
        return response!.Data.FirstOrDefault(c => string.Equals(c.Symbol, symbol, StringComparison.CurrentCultureIgnoreCase));
    }
    
    /// <summary>
    /// Gets the currency with the specified name.
    /// </summary>
    /// <param name="name">Name of the currency (e.g. Bitcoin)</param>
    /// <returns>Currency object if such currency is found, otherwise null</returns>
    public static Currency? GetCurrencyByName(string name)
    {
        var url = $"{_baseUrl}/assets?search={name}";
        var json = GetResponse(url);
        var response = JsonSerializer.Deserialize<ApiResponse<IEnumerable<Currency>>>(json);
        EnsureDeserialization(response);
        return response!.Data.FirstOrDefault(c => string.Equals(c.Name, name, StringComparison.CurrentCultureIgnoreCase));
    }

    /// <summary>
    /// Gets the markets for the specified currency.
    /// </summary>
    /// <param name="currencyId">ID of the currency</param>
    /// <returns>A list of markets where the specified curency may be purchased.</returns>
    public static IEnumerable<Market> GetMarkets(string currencyId)
    {
        var url = $"{_baseUrl}/assets/{currencyId}/markets";
        var json = GetResponse(url);
        var response = JsonSerializer.Deserialize<ApiResponse<IEnumerable<Market>>>(json);
        EnsureDeserialization(response);
        return response!.Data;
    }
    
    /// <summary>
    /// Gets the price history for the specified currency.
    /// </summary>
    /// <param name="currencyId">ID of the currency</param>
    /// <param name="interval">Interval between two points on plot</param>
    /// <returns>A list of prices that the currency had over the last time.</returns>
    public static IEnumerable<Price> GetPriceHistory(string currencyId, string interval = "d1")
    {
        var url = $"{_baseUrl}/assets/{currencyId}/history?interval={interval}";
        var json = GetResponse(url);
        var response = JsonSerializer.Deserialize<ApiResponse<IEnumerable<Price>>>(json);
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