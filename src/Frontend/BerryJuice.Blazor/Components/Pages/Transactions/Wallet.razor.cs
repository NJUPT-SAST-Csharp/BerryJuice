using Accounts.Application.AccountService.Models;
using Accounts.Application.AccountService.Queries;
using Accounts.Application.TransactionService.Commands;
using Accounts.Application.TransactionService.Queries;
using Enums;

namespace BerryJuice.Blazor.Components.Pages.Transactions;

public partial class Wallet
{
    public long SelectedAccountId { get; set; } = 0;

    public string? MoneyInput { get; set; }
    private List<AccountModel> _accounts = [];

    protected override async Task OnInitializedAsync()
    {
        await UpdateAccountListAsync();
    }

    private async Task UpdateAccountListAsync()
    {
        _accounts = (await EventBus.QueryAsync(new GetAccountsQuery())).Accounts.ToList();
        StateHasChanged();
    }

    private async Task AddTransactionAsync()
    {
        if (SelectedAccountId != 0 && Decimal.TryParse(MoneyInput, out var moneyValue))
        {
            var transactionId = await EventBus.CommandAsync(
                new AddTransactionCommand(SelectedAccountId, CurrencyType.CNY, moneyValue, [], description: "test")
            );
        }
    }
}
