namespace Asset.Domain.AssetAggregate.AssetEntity;

public readonly record struct AssetId(long Value)
{
    public override string ToString()
    {
        return Value.ToString();
    }
}