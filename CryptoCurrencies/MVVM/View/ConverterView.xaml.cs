using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CryptoCurrencies.Net;

namespace CryptoCurrencies.MVVM.View;

public partial class ConverterView : UserControl
{
    public ConverterView()
    {
        InitializeComponent();
    }

    private void ConvertCurrencies(object sender, RoutedEventArgs e)
    {
        var fromCurrencyCode = FromInput.Text;
        var toCurrencyCode = ToInput.Text;
        if (!double.TryParse(QuantityInput.Text, CultureInfo.InvariantCulture, out var quantity))
        {
            MessageBox.Show("Please enter a valid quantity.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        
        if (string.IsNullOrWhiteSpace(fromCurrencyCode) || string.IsNullOrWhiteSpace(toCurrencyCode))
        {
            MessageBox.Show("Please enter the currency codes.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        
        if (quantity <= 0)
        {
            MessageBox.Show("Please enter a valid quantity.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        
        if (fromCurrencyCode == toCurrencyCode)
        {
            MessageBox.Show("The currencies must be different.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        var fromCurrency = ApiTools.GetCurrencyBySymbol(fromCurrencyCode);
        if (fromCurrency is null)
        {
            MessageBox.Show($"The currency code is invalid: {fromCurrencyCode}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        
        var toCurrency = ApiTools.GetCurrencyBySymbol(toCurrencyCode);
        if (toCurrency is null)
        {
            MessageBox.Show($"The currency code is invalid: {toCurrencyCode}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        
        var result = fromCurrency.Convert(toCurrency, quantity);
        var roundedResult = Math.Round(result, 2);
        ResultText.Text = $"{fromCurrencyCode} is {roundedResult} {toCurrencyCode}";
    }
}