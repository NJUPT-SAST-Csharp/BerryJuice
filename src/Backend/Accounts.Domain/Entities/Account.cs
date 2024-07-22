using System.ComponentModel.DataAnnotations;
using Accounts.Domain.ValueObjects;
using Primitives.Entity;
using Shared.Primitives;

namespace Accounts.Domain.Entities;

public class Account(DateTime date, decimal amount, string tag, string? description)
    : EntityBase<AccountId>(new AccountId(1)), IAggregateRoot<Account>
{
    public DateTime Date { get; set; } = date;

    [Required] public decimal Amount { get; set; } = amount;
    [Required][MaxLength(100)] public string Tag { get; set; } = tag;

    [MaxLength(500)] public string? Description { get; set; } = description;
}