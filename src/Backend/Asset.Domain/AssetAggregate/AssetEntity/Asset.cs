using Asset.Domain.AssetAggregate.AdjustmentRecordEntity;
using Asset.Domain.AssetAggregate.AssetEntity.Event;
using Primitives.Entity;
using Shared.Primitives;

namespace Asset.Domain.AssetAggregate.AssetEntity;

public class Asset : EntityBase<AssetId>, IAggregateRoot<Asset>
{
    private Asset(string name, decimal balance) : base(new AssetId(1))
    {
        _name = name;
        _balance = balance;
        _adjustmentRecords = [];
    }

    public static Asset CreateNewAsset(string name,decimal balance)
    {
        var asset = new Asset(name,balance);
        asset.AddDomainEvent(new AssetCreatedDomainEvent(asset.Id));
        return asset;
    }

    private string _name;
    private decimal _balance; //余额
    private List<AdjustmentRecord> _adjustmentRecords; //校准的记录
}