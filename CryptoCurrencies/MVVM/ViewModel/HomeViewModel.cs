using System.Globalization;
using System.Windows;
using System.Windows.Media;
using CryptoCurrencies.Core;
using CryptoCurrencies.Models;
using CryptoCurrencies.Net;

namespace CryptoCurrencies.MVVM.ViewModel;

public class HomeViewModel : ObservableObject
{
    public static event EventHandler<CurrencyEventArgs>? CurrencySelected; // Marked as static because it does not work on instances of the class for some reason
    
    private List<Currency> _currencies;
    
    public HomeViewModel()
    {
        _currencies = ApiTools.GetTop10Currencies().ToList();
    }
    
    public List<Currency> Currencies
    {
        get => _currencies;
        set
        {
            _currencies = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand CurrencySelectedCommand => new RelayCommand(param =>
    {
        if (param is not Currency currency)
        {
            throw new ArgumentException("The parameter must be a Currency object.", nameof(param));
        }
        CurrencySelected?.Invoke(this, new CurrencyEventArgs(currency));
    });
}