using System.Windows;
using CryptoCurrencies.Core;
using CryptoCurrencies.Models;

namespace CryptoCurrencies.MVVM.ViewModel;

public class MainViewModel : ObservableObject
{
    private object _currentViewModel;
    private Currency? _selectedCurrency;
    

    public MainViewModel()
    {
        HomeViewModel = new HomeViewModel();
        HomeViewModel.CurrencySelected += OnCurrencySelected;
        CurrentViewModel = HomeViewModel;
    }
    
    public void OnCurrencySelected(object? sender, CurrencySelectedEventArgs args)
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
    
    public Currency? SelectedCurrency
    {
        get => _selectedCurrency;
        set
        {
            _selectedCurrency = value;
            OnPropertyChanged();
        }
    }

    public HomeViewModel HomeViewModel { get; set; }

    public RelayCommand ExitCommand
    {
        get => new RelayCommand(_ => Application.Current.Shutdown());
        set => throw new NotImplementedException();
    }

    public RelayCommand HomeViewCommand
    {
        get => new RelayCommand(_ => CurrentViewModel = HomeViewModel);
        set => throw new NotImplementedException();
    }

    public RelayCommand CurrencyViewCommand
    {
        get => new RelayCommand(_ => CurrentViewModel = "CurrencyViewModel", _ => SelectedCurrency is not null);
        set => throw new NotImplementedException();
    }

    public RelayCommand ConverterViewCommand
    {
        get => new RelayCommand(_ => CurrentViewModel = "ConverterViewModel", _ => SelectedCurrency is not null);
        set => throw new NotImplementedException();
    }
}