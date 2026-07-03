namespace vantagePMO_platform.TaskCollaboration.Domain.Model.Queries;

public record GetAllBoardsQuery;

public record GetBoardMembersByBoardQuery(int BoardId);

public record GetCollaborationTasksQuery(int? BoardId);
