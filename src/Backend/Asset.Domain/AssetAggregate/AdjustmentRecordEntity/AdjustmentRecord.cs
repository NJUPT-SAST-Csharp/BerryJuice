using Asset.Domain.AssetAggregate.AssetEntity;
using Asset.Domain.AssetAggregate.AssetEntity.Events;
using Primitives.Entity;
using Utilities;

namespace Asset.Domain.AssetAggregate.AdjustmentRecordEntity;

public class AdjustmentRecord : EntityBase<AdjustmentRecordId>
{
    private DateTime _adjustmentDate;
    private decimal _amount;


    private readonly AssetId _assetId;
    private string? _reason;

    private AdjustmentRecord(AssetId assetId, decimal amount, string? reason, DateTime adjustmentDate) : base(
        new AdjustmentRecordId(SnowFlakeIdGenerator.NewId)
    )
    {
        _assetId = assetId;
        _amount = amount;
        _reason = reason;
        _adjustmentDate = DateTime.UtcNow;
    }

    public static AdjustmentRecord CreateNewAdjustmentRecord(
        AssetId assetId,
        decimal amount,
        string? reason,
        DateTime adjustmentDate
    )
    {
        var record = new AdjustmentRecord(assetId, amount, reason, adjustmentDate);
        record.AddDomainEvent(new AssetAdjustedDomainEvent(record._assetId, record.Id));
        return record;
    }
}
