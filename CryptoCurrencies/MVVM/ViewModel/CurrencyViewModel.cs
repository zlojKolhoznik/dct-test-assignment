using System.Globalization;
using System.Windows;
using CryptoCurrencies.Core;
using CryptoCurrencies.Models;
using CryptoCurrencies.Net;

namespace CryptoCurrencies.MVVM.ViewModel;

public class CurrencyViewModel : ObservableObject
{
    private Currency? _currency;
    private List<Market>? _markets;
    private List<PriceHistoryPoint>? _priceHistory;

    public CurrencyViewModel()
    {
        HomeViewModel.CurrencySelected += OnCurrencySelected;
        Currency = MainViewModel.SelectedCurrency;
        if (Currency is not null)
        {
            Markets = ApiTools.GetMarkets(Currency.Id).Take(5).ToList();
            PriceHistory = ApiTools.GetPriceHistory(Currency.Id)
                .OrderByDescending(p => p.Time)
                .Take(30)
                .Select(p => new PriceHistoryPoint(p.PriceUsdDouble.ToString(CultureInfo.InvariantCulture), DateTime.Parse(p.Date).ToShortDateString())).ToList();
        }
    }

    private void OnCurrencySelected(object? sender, CurrencyEventArgs e)
    {
        Currency = e.Currency;
        Markets = ApiTools.GetMarkets(Currency.Id).Take(5).ToList();
    }
    
    public List<PriceHistoryPoint>? PriceHistory
    {
        get => _priceHistory;
        set
        {
            _priceHistory = value;
            OnPropertyChanged();
        }
    }

    public Currency? Currency
    {
        get => _currency;
        set
        {
            _currency = value;
            OnPropertyChanged();
        }
    }

    public List<Market>? Markets
    {
        get => _markets;
        set
        {
            _markets = value;
            OnPropertyChanged();
        }
    }
}
public record PriceHistoryPoint(string Price, string Date);