using CryptoCurrencies.Models;

namespace CryptoCurrencies.Core;

public class CurrencyEventArgs : EventArgs
{
    public CurrencyEventArgs(Currency currency)
    {
        Currency = currency;
    }

    public Currency Currency { get; set; }
}