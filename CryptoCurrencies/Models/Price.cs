using System.Text.Json.Serialization;

namespace CryptoCurrencies.Models;

public class Price
{
    [JsonPropertyName("priceUsd")]
    public string PriceUsd { get; set; }
    
    [JsonPropertyName("time")]
    public string Time { get; set; }
}