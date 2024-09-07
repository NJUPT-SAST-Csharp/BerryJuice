using Accounts.Application.TransactionService.Commands;
using Enums;
using Microsoft.AspNetCore.Components;

namespace BerryJuice.Blazor.Components.Pages.Transactions;

public partial class CreateNewTransaction : ComponentBase
{
    [CascadingParameter(Name = "AccountId")]
    public long Id { get; set; }

    public string? MoneyInput { get; set; }

    private async Task AddTransactionAsync()
    {
        if (Id != 0 && Decimal.TryParse(MoneyInput, out var moneyValue))
        {
            var transactionId = await EventBus.CommandAsync(
                new AddTransactionCommand(Id, CurrencyType.CNY, moneyValue, [], description: "test")
            );
        }
    }
}
