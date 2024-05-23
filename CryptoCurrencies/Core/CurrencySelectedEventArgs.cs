using CryptoCurrencies.Models;

namespace CryptoCurrencies.Core;

public class CurrencySelectedEventArgs : EventArgs
{
    public CurrencySelectedEventArgs(Currency currency)
    {
        Currency = currency;
    }

    public Currency Currency { get; set; }
}