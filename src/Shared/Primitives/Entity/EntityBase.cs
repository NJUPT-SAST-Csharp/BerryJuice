using Primitives.DomainEvent;
using Primitives.Rule;

namespace Primitives.Entity;

public abstract class EntityBase<T> : IEquatable<EntityBase<T>>, IDomainEventContainer, IEntity<T>
    where T : IEquatable<T>
{
    private readonly List<IDomainEvent> _domainEvents = [];

    protected EntityBase(T id)
    {
        Id = id;
    }

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public T Id { get; }

    public bool Equals(EntityBase<T>? other)
    {
        if (other is null)
        {
            return false;
        }

        return other.Id.Equals(Id);
    }

    public static bool operator ==(EntityBase<T> left, EntityBase<T> right)
    {
        return ReferenceEquals(left, right) || (left is not null && right is not null && left.Equals(right));
    }

    public static bool operator !=(EntityBase<T> left, EntityBase<T> right)
    {
        return !(left == right);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as EntityBase<T>);
    }

    protected static void CheckRule<TRule>(in TRule rule)
        where TRule : IDomainBusinessRule
    {
        if (rule.IsBroken)
        {
            throw new DomainBusinessRuleInvalidException(rule, typeof(TRule).Name);
        }
    }

    protected static async Task CheckRuleAsync<TAsyncRule>(TAsyncRule rule)
        where TAsyncRule : IAsyncDomainBusinessRule
    {
        if (await rule.IsBrokenAsync())
        {
            throw new DomainBusinessRuleInvalidException(rule, typeof(TAsyncRule).Name);
        }
    }
}
