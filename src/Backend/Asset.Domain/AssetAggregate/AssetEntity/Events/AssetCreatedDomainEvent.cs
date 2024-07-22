using Shared.Primitives.DomainEvent;

namespace Asset.Domain.AssetAggregate.AssetEntity.Event;

public class AssetCreatedDomainEvent(AssetId assetId) : IDomainEvent
{
    public AssetId AssetId { get; } = assetId;
}