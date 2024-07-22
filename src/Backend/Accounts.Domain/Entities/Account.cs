using System.ComponentModel.DataAnnotations;
using Accounts.Domain.ValueObjects;
using Primitives.Entity;
using Shared.Primitives;

namespace Accounts.Domain.Entities;

public class Account(DateTime date, decimal amount, MethodOfPayment methodOfPayment, string tag, string? description)
    : EntityBase<AccountId>(new AccountId(1)), IAggregateRoot<Account>
{
    public DateTime Date { get; set; } = date;

    public decimal Amount { get; set; } = amount;

    public MethodOfPayment Method { get; set; } = methodOfPayment;
    public string Tag { get; set; } = tag;

    public string? Description { get; set; } = description;
}