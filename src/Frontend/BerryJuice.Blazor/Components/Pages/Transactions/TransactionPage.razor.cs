using Accounts.Application.AccountService.Models;
using Accounts.Application.AccountService.Queries;
using Accounts.Application.TransactionService.Commands;
using Accounts.Application.TransactionService.Queries;
using Enums;

namespace BerryJuice.Blazor.Components.Pages.Transactions;

public partial class TransactionPage
{
    public long SelectedAccountId { get; set; } = 0;

    private List<AccountModel> _accounts = [];

    protected override async Task OnInitializedAsync()
    {
        await UpdateAccountListAsync();
        if (_accounts.Count > 0) SelectedAccountId = _accounts[0].Id;
    }

    private async Task UpdateAccountListAsync()
    {
        _accounts = (await EventBus.QueryAsync(new GetAccountsQuery())).Accounts.ToList();
        StateHasChanged();
    }


}
