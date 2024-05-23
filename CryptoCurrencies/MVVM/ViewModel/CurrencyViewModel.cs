using System.Windows;
using CryptoCurrencies.Core;
using CryptoCurrencies.Models;
using CryptoCurrencies.Net;

namespace CryptoCurrencies.MVVM.ViewModel;

public class CurrencyViewModel : ObservableObject
{
    private Currency? _currency;
    private List<Market>? _markets;

    public CurrencyViewModel()
    {
        HomeViewModel.CurrencySelected += OnCurrencySelected;
        Currency = MainViewModel.SelectedCurrency;
        if (Currency is not null)
        {
            Markets = ApiTools.GetMarkets(Currency.Id).Take(5).ToList();
        }
    }

    private void OnCurrencySelected(object? sender, CurrencyEventArgs e)
    {
        Currency = e.Currency;
        Markets = ApiTools.GetMarkets(Currency.Id).Take(5).ToList();
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