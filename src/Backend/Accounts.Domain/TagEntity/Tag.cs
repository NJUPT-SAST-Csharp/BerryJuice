using Accounts.Domain.AccountAggregate.TransactionEntity;
using Primitives.Entity;
using Utilities;

namespace Accounts.Domain.TagEntity;

public class Tag : EntityBase<TagId>, IAggregateRoot<Tag>
{
    private string _name;

    private List<Transaction> _transactions = [];

    private Tag(string name) : base(new TagId(SnowFlakeIdGenerator.NewId))
    {
        _name = name;
    }

    public static Tag CreateNewTag(string name)
    {
        var tag = new Tag(name);
        return tag;
    }
}
