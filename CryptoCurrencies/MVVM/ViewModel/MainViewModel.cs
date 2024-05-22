using System.Windows;
using CryptoCurrencies.Core;

namespace CryptoCurrencies.MVVM.ViewModel;

public class MainViewModel : ObservableObject
{
    public RelayCommand ExitCommand => new RelayCommand(_ => Application.Current.Shutdown());
}