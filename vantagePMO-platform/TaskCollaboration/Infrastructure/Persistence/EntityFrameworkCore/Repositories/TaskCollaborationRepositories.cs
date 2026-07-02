using Microsoft.EntityFrameworkCore;
using vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using vantagePMO_platform.TaskCollaboration.Domain.Model.Aggregates;
using vantagePMO_platform.TaskCollaboration.Domain.Repositories;

namespace vantagePMO_platform.TaskCollaboration.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class BoardRepository(AppDbContext context)
    : BaseRepository<Board>(context), IBoardRepository
{
    public async Task<bool> AnyAsync(CancellationToken cancellationToken = default) =>
        await context.Set<Board>().AnyAsync(cancellationToken);
}

public class BoardMemberRepository(AppDbContext context) : IBoardMemberRepository
{
    public async Task<IReadOnlyList<BoardMember>> ListByBoardIdAsync(int boardId, CancellationToken cancellationToken = default) =>
        await context.Set<BoardMember>()
            .Where(member => member.BoardId == boardId)
            .OrderBy(member => member.Id)
            .ToListAsync(cancellationToken);

    public async Task AddRangeAsync(IEnumerable<BoardMember> members, CancellationToken cancellationToken = default) =>
        await context.Set<BoardMember>().AddRangeAsync(members, cancellationToken);

    public async Task<bool> AnyAsync(CancellationToken cancellationToken = default) =>
        await context.Set<BoardMember>().AnyAsync(cancellationToken);
}

public class CollaborationTaskRepository(AppDbContext context)
    : BaseRepository<CollaborationTask>(context), ICollaborationTaskRepository
{
    public async Task<IReadOnlyList<CollaborationTask>> ListByBoardIdAsync(int boardId, CancellationToken cancellationToken = default) =>
        await context.Set<CollaborationTask>()
            .Where(task => task.BoardId == boardId)
            .OrderBy(task => task.Id)
            .ToListAsync(cancellationToken);

    public async Task<bool> AnyAsync(CancellationToken cancellationToken = default) =>
        await context.Set<CollaborationTask>().AnyAsync(cancellationToken);
}
