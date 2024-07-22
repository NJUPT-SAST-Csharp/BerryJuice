namespace Asset.Domain.AssetAggregate.AdjustmentRecordEntity;

public readonly record struct AdjustmentRecordId(long Value)
{
    public override string ToString()
    {
        return Value.ToString();
    }
}