namespace Accounts.Domain.TagEntity;

public interface ITagRepository
{
    Task<TagId> AddTagAsync(Tag tag, CancellationToken cancellationToken = default);
    Task<Tag> GetTagAsync(TagId id, CancellationToken cancellationToken = default);
}