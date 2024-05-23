using System.Globalization;
using System.Text.Json.Serialization;

namespace CryptoCurrencies.Models;

public class Price
{
    [JsonPropertyName("priceUsd")]
    public string PriceUsd { get; set; }
    
    [JsonPropertyName("time")]
    public long Time { get; set; }
    
    [JsonPropertyName("date")]
    public string Date { get; set; }
    
    public double PriceUsdDouble => (double)Math.Round(decimal.Parse(PriceUsd, CultureInfo.InvariantCulture), 2);
}