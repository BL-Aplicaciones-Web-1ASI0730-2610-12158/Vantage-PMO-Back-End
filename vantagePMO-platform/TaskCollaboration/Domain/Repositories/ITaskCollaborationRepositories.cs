using vantagePMO_platform.TaskCollaboration.Domain.Model.Aggregates;
using vantagePMO_platform.Shared.Domain.Repositories;

namespace vantagePMO_platform.TaskCollaboration.Domain.Repositories;

public interface IBoardRepository : IBaseRepository<Board>
{
    Task<bool> AnyAsync(CancellationToken cancellationToken = default);
}

public interface IBoardMemberRepository
{
    Task<IReadOnlyList<BoardMember>> ListByBoardIdAsync(int boardId, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<BoardMember> members, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(CancellationToken cancellationToken = default);
}

public interface ICollaborationTaskRepository : IBaseRepository<CollaborationTask>
{
    Task<IReadOnlyList<CollaborationTask>> ListByBoardIdAsync(int boardId, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(CancellationToken cancellationToken = default);
}
