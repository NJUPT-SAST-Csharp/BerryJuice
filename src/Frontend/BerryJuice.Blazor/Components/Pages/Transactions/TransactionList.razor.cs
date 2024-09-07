using Accounts.Application.TransactionService.Models;
using Accounts.Application.TransactionService.Queries;
using Microsoft.AspNetCore.Components;
using Enums;

namespace BerryJuice.Blazor.Components.Pages.Transactions;

public partial class TransactionList : ComponentBase
{
    [CascadingParameter(Name = "AccountId")]
    public long Id
    {
        get => _id;
        set
        {
            if (value != _id && value > 0)
            {
                _ = UpdateTransactionListAsync(value);
                _id = value;
            }
        }
    }

    private async Task UpdateTransactionListAsync(long id)
    {
        _transactions = (await EventBus.QueryAsync(new GetTransactionsQuery(id))).Transactions.ToList();
        StateHasChanged();
    }

    private List<TransactionModel> _transactions = [];
    private long _id;
}
