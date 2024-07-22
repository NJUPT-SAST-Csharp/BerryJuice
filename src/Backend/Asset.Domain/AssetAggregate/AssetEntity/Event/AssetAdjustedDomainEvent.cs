using Asset.Domain.AssetAggregate.AdjustmentRecordEntity;
using Shared.Primitives.DomainEvent;

namespace Asset.Domain.AssetAggregate.AssetEntity.Event;

public class AssetAdjustedDomainEvent(AssetId assetId, AdjustmentRecordId adjustmentRecordId) : IDomainEvent
{
    public AssetId AssetId { get; } = assetId;

    public AdjustmentRecordId AdjustmentRecordId { get; } = adjustmentRecordId;
}