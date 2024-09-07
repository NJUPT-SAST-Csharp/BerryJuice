using Accounts.Domain.TagEntity;
using Accounts.Infrastructure.Persistence;
using Exceptions.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Infrastructure.DomainRepository;

internal class TagRepository(AccountsContext context) : ITagRepository
{
    private readonly AccountsContext _context = context;

    public async Task<TagId> AddTagAsync(Tag tag, CancellationToken cancellationToken = default)
    {
        var t = await _context.Tags.AddAsync(tag, cancellationToken);
        return t.Entity.Id;
    }

    public async Task<Tag> GetTagAsync(TagId id, CancellationToken cancellationToken = default)
    {
        var t = await _context
                     .Tags.Where<Tag>(t => t.Id == id)
                     .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        return t ?? throw new DbNotFoundException(nameof(Tag), id.Value.ToString());
    }
}