using vantagePMO_platform.TaskCollaboration.Application.QueryServices;
using vantagePMO_platform.TaskCollaboration.Domain.Model.Aggregates;
using vantagePMO_platform.TaskCollaboration.Domain.Model.Queries;
using vantagePMO_platform.TaskCollaboration.Domain.Repositories;

namespace vantagePMO_platform.TaskCollaboration.Application.Internal.QueryServices;

public class BoardQueryService(IBoardRepository boardRepository) : IBoardQueryService
{
    public async Task<IEnumerable<Board>> Handle(GetAllBoardsQuery query, CancellationToken cancellationToken = default) =>
        await boardRepository.ListAsync(cancellationToken);
}

public class BoardMemberQueryService(IBoardMemberRepository boardMemberRepository) : IBoardMemberQueryService
{
    public async Task<IReadOnlyList<BoardMember>> Handle(
        GetBoardMembersByBoardQuery query,
        CancellationToken cancellationToken = default) =>
        await boardMemberRepository.ListByBoardIdAsync(query.BoardId, cancellationToken);
}

public class CollaborationTaskQueryService(ICollaborationTaskRepository collaborationTaskRepository) : ICollaborationTaskQueryService
{
    public async Task<IReadOnlyList<CollaborationTask>> Handle(
        GetCollaborationTasksQuery query,
        CancellationToken cancellationToken = default)
    {
        if (query.BoardId.HasValue)
            return await collaborationTaskRepository.ListByBoardIdAsync(query.BoardId.Value, cancellationToken);

        var tasks = await collaborationTaskRepository.ListAsync(cancellationToken);
        return tasks.OrderBy(task => task.Id).ToList();
    }
}
