using Accounts.Domain.AccountAggregate.Services;
using Enums;

namespace Accounts.Domain.AccountAggregate.TransactionEntity;

public record struct TransactionAmount(
    CurrencyType Currency = CurrencyType.CNY,
    decimal Amount = 0m
)
{
    public static TransactionAmount operator +(TransactionAmount a, TransactionAmount b)
    {
        return new TransactionAmount(
            a.Currency,
            a.Amount + CurrencyDomainService.Convert(b.Amount, b.Currency, a.Currency)
        );
    }

    public static TransactionAmount operator -(TransactionAmount a, TransactionAmount b)
    {
        if (a.Currency != b.Currency)
        {
            throw new InvalidOperationException(
                message: "Cannot subtract amounts with different currencies"
            );
        }

        return new TransactionAmount(
            a.Currency,
            a.Amount - CurrencyDomainService.Convert(b.Amount, b.Currency, a.Currency)
        );
    }

    public override string ToString()
    {
        return CurrencyDomainService.GetCurrencyString(Amount, Currency);
    }
}
