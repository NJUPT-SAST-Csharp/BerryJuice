namespace Primitives.Rule;

public interface IAsyncDomainBusinessRule
{
    public string Message { get; }
    public Task<bool> IsBrokenAsync();
}
