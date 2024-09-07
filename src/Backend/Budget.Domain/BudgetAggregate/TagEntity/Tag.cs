using Primitives.Entity;
using Utilities;

namespace Budget.Domain.BudgetAggregate.TagEntity;

public class Tag : EntityBase<TagId>, IAggregateRoot<Tag>
{
    private string _name;

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
