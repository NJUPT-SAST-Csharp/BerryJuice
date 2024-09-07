using Enums;

namespace Accounts.Domain.AccountAggregate.Services;

internal static class CurrencyDomainService
{
    // For test purposes, use a fixed conversion rate (USD JPY CNY HKD)
    private static readonly Dictionary<(CurrencyType, CurrencyType), decimal> _conversionRates = new()
    {
        [(CurrencyType.USD, CurrencyType.USD)] = 1,
        [(CurrencyType.USD, CurrencyType.JPY)] = 110,
        [(CurrencyType.USD, CurrencyType.CNY)] = 6.5m,
        [(CurrencyType.USD, CurrencyType.HKD)] = 7.8m,
        [(CurrencyType.JPY, CurrencyType.USD)] = 0.0091m,
        [(CurrencyType.JPY, CurrencyType.JPY)] = 1,
        [(CurrencyType.JPY, CurrencyType.CNY)] = 0.059m,
        [(CurrencyType.JPY, CurrencyType.HKD)] = 0.071m,
        [(CurrencyType.CNY, CurrencyType.USD)] = 0.15m,
        [(CurrencyType.CNY, CurrencyType.JPY)] = 16.9m,
        [(CurrencyType.CNY, CurrencyType.CNY)] = 1,
        [(CurrencyType.CNY, CurrencyType.HKD)] = 1.2m,
        [(CurrencyType.HKD, CurrencyType.USD)] = 0.13m,
        [(CurrencyType.HKD, CurrencyType.JPY)] = 14.1m,
        [(CurrencyType.HKD, CurrencyType.CNY)] = 0.83m,
        [(CurrencyType.HKD, CurrencyType.HKD)] = 1,
    };

    public static decimal Convert(decimal amount, CurrencyType fromCurrency, CurrencyType toCurrency)
    {
        return amount * _conversionRates[(fromCurrency, toCurrency)];
    }

    public static string GetCurrencyString(decimal amount, CurrencyType currency)
    {
        return currency switch
        {
            CurrencyType.USD => $"$ {amount}",
            CurrencyType.CNY => $"{amount} ¥",
            CurrencyType.HKD => $"HK$ {amount}",
            CurrencyType.JPY => $"{amount} ¥",
            _                => $"{amount}",
        };
    }
}
