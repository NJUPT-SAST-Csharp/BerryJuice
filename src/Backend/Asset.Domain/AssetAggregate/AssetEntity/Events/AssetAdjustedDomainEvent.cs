using Asset.Domain.AssetAggregate.AdjustmentRecordEntity;
using Primitives.DomainEvent;

namespace Asset.Domain.AssetAggregate.AssetEntity.Events;

public class AssetAdjustedDomainEvent(
    AssetId assetId,
    AdjustmentRecordId adjustmentRecordId
) : IDomainEvent
{
    public AssetId AssetId { get; } = assetId;

    public AdjustmentRecordId AdjustmentRecordId { get; } = adjustmentRecordId;
}
