using Primitives.DomainEvent;

namespace Asset.Domain.AssetAggregate.AssetEntity.Events;

public class AssetCreatedDomainEvent(
    AssetId assetId
) : IDomainEvent
{
    public AssetId AssetId { get; } = assetId;
}
