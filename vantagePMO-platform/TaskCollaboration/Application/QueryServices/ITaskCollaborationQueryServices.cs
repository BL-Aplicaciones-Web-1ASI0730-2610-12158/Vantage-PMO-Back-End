using vantagePMO_platform.TaskCollaboration.Domain.Model.Aggregates;
using vantagePMO_platform.TaskCollaboration.Domain.Model.Queries;

namespace vantagePMO_platform.TaskCollaboration.Application.QueryServices;

public interface IBoardQueryService
{
    Task<IEnumerable<Board>> Handle(GetAllBoardsQuery query, CancellationToken cancellationToken = default);
}

public interface IBoardMemberQueryService
{
    Task<IReadOnlyList<BoardMember>> Handle(GetBoardMembersByBoardQuery query, CancellationToken cancellationToken = default);
}

public interface ICollaborationTaskQueryService
{
    Task<IReadOnlyList<CollaborationTask>> Handle(GetCollaborationTasksQuery query, CancellationToken cancellationToken = default);
}
