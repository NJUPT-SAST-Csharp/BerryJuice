using Accounts.Domain.AccountAggregate.TransactionEntity;
using Primitives.Entity;
using Shared.Primitives;
using Utilities;

namespace Accounts.Domain.TagEntity;

public class Tag : EntityBase<TagId>, IAggregateRoot<Tag>
{
    private Tag(string name)
        : base(new TagId(SnowFlakeIdGenerator.NewId))
    {
        _name = name;
    }

    public static Tag CreateNewTag(string name)
    {
        var tag = new Tag(name);
        return tag;
    }

    private string _name;

    private List<Transaction> _transactions = [];
}