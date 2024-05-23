using System.Globalization;
using System.Text.Json.Serialization;

namespace CryptoCurrencies.Models;

public class Currency
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    
    [JsonPropertyName("rank")]
    public string Rank { get; set; }
    
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("supply")]
    public string Supply { get; set; }
    
    [JsonPropertyName("maxSupply")]
    public string MaxSupply { get; set; }
    
    [JsonPropertyName("marketCapUsd")]
    public string MarketCapUsd { get; set; }
    
    [JsonPropertyName("volumeUsd24Hr")]
    public string VolumeUsd24Hr { get; set; }
    
    [JsonPropertyName("priceUsd")]
    public string PriceUsd { get; set; }
    
    [JsonPropertyName("changePercent24Hr")]
    public string ChangePercent24Hr { get; set; }
    
    [JsonPropertyName("vwap24Hr")]
    public string Vwap24Hr { get; set; }
    
    public double Convert(Currency other, double amount)
    {
        return amount * double.Parse(PriceUsd) / double.Parse(other.PriceUsd);
    }

    public double PriceUsdDouble => (double)Math.Round(decimal.Parse(PriceUsd, CultureInfo.InvariantCulture), 2);
    public double VolumeUsd24HrDouble => (double)Math.Round(decimal.Parse(VolumeUsd24Hr, CultureInfo.InvariantCulture), 2);
    public double SupplyDouble => (double)Math.Round(decimal.Parse(Supply, CultureInfo.InvariantCulture), 2);
    public double MarketCapUsdDouble => (double)Math.Round(decimal.Parse(MarketCapUsd, CultureInfo.InvariantCulture), 2);
}