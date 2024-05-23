using System.Windows;
using CryptoCurrencies.Core;
using CryptoCurrencies.Models;
using CryptoCurrencies.Net;

namespace CryptoCurrencies.MVVM.ViewModel;

public class MainViewModel : ObservableObject
{
    private object _currentViewModel;


    public MainViewModel()
    {
        HomeViewModel = new HomeViewModel();
        CurrencyViewModel = new CurrencyViewModel();
        ConverterViewModel = new ConverterViewModel();
        HomeViewModel.CurrencySelected += OnCurrencySelected;
        CurrentViewModel = HomeViewModel;
    }
    
    public void OnCurrencySelected(object? sender, CurrencyEventArgs args)
    {
        SelectedCurrency = args.Currency;
        CurrencyViewCommand.Execute(null);
    }

    public object CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel = value;
            OnPropertyChanged();
        }
    }
    
    public static Currency? SelectedCurrency { get; set; }

    public HomeViewModel HomeViewModel { get; set; }

    public CurrencyViewModel CurrencyViewModel { get; set; }
    
    public ConverterViewModel ConverterViewModel { get; set; }

    public RelayCommand ExitCommand => new RelayCommand(_ => Application.Current.Shutdown());

    public RelayCommand HomeViewCommand => new RelayCommand(_ => CurrentViewModel = HomeViewModel);

    public RelayCommand CurrencyViewCommand => new RelayCommand(_ => CurrentViewModel = CurrencyViewModel, _ => SelectedCurrency is not null);

    public RelayCommand ConverterViewCommand => new RelayCommand(_ => CurrentViewModel = ConverterViewModel);
    
    public RelayCommand SearchCommand => new RelayCommand(async param =>
    {
        CurrentViewModel = new LoadingViewModel();
        await Task.Delay(100);
        if (param is not string query)
        {
            throw new ArgumentException("The parameter must be a string.", nameof(param));
        }

        if (string.IsNullOrWhiteSpace(query))
        {
            throw new ArgumentException("Search query must be at least one character long.", nameof(param));
        }

        var byName = ApiTools.GetCurrencyByName(query);
        if (byName is not null)
        {
            SelectedCurrency = byName;
            CurrencyViewCommand.Execute(null);
            return;
        }
        
        var bySymbol = ApiTools.GetCurrencyBySymbol(query);
        if (bySymbol is not null)
        {
            SelectedCurrency = bySymbol;
            CurrencyViewCommand.Execute(null);
            return;
        }
        
        MessageBox.Show("No currency found with that name or symbol.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        HomeViewCommand.Execute(null);
    });
}