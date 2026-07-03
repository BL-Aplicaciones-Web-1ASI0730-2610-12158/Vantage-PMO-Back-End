using vantagePMO_platform.Shared.Domain.Repositories;
using vantagePMO_platform.TaskCollaboration.Domain.Model.Aggregates;
using vantagePMO_platform.TaskCollaboration.Domain.Model.Commands;
using vantagePMO_platform.TaskCollaboration.Domain.Repositories;

namespace vantagePMO_platform.TaskCollaboration.Application.Internal.CommandServices;

/// <summary>
///     Seeds boards, members and collaboration tasks (matches front-end db.json).
/// </summary>
public class TaskCollaborationSampleDataSeeder(
    IBoardRepository boardRepository,
    IBoardMemberRepository boardMemberRepository,
    ICollaborationTaskRepository collaborationTaskRepository,
    IUnitOfWork unitOfWork)
{
    public async Task SeedIfEmptyAsync(CancellationToken cancellationToken = default)
    {
        if (!await boardRepository.AnyAsync(cancellationToken))
        {
            await boardRepository.AddAsync(new Board("Operativa Team Board", 4, "Cross-functional operational tasks"), cancellationToken);
            await boardRepository.AddAsync(new Board("Neo-Nexus Project", 2, "Next-gen infrastructure initiative"), cancellationToken);
            await boardRepository.AddAsync(new Board("Design Sprint Q2", 3, "UX and visual design workstream"), cancellationToken);
        }

        if (!await boardMemberRepository.AnyAsync(cancellationToken))
        {
            await boardMemberRepository.AddRangeAsync(
            [
                new BoardMember(1, "Alex Sterling", "Lead Architect", "AS", "#3b82f6", "online"),
                new BoardMember(1, "Sarah Jenkins", "Senior Designer", "SJ", "#8b5cf6", "online"),
                new BoardMember(1, "John Doe", "Backend Dev", "JD", "#10b981", "busy"),
                new BoardMember(1, "Marcus R.", "Finance Lead", "MR", "#f59e0b", "offline"),
                new BoardMember(1, "David L.", "Frontend Dev", "DL", "#ef4444", "online"),
                new BoardMember(2, "Elena V.", "PM", "EV", "#06b6d4", "online"),
                new BoardMember(2, "Ryan T.", "DevOps", "RT", "#ec4899", "busy"),
                new BoardMember(3, "Lucia M.", "UX Designer", "LM", "#a855f7", "online")
            ],
                cancellationToken);
        }

        if (!await collaborationTaskRepository.AnyAsync(cancellationToken))
        {
            foreach (var command in SampleTasks())
                await collaborationTaskRepository.AddAsync(new CollaborationTask(command), cancellationToken);
        }

        await unitOfWork.CompleteAsync(cancellationToken);
    }

    private static IEnumerable<CreateCollaborationTaskCommand> SampleTasks() =>
    [
        Task(1, "PROJECT::HORIZON", "Finalize Q3 Architectural Specs for Dubai Mall Expansion", "Complete technical documentation for all structural components.", "Alex Sterling", "AS", "#3b82f6", "To Do", "URGENT", "#ef4444", false, 4, 2, "", 0, "Engineering"),
        Task(1, "PROJECT::NEO-LIFE", "Structural integrity simulation for Phase 2 bridges", "Run FEA simulations on updated bridge design parameters.", "Marcus R.", "MR", "#f59e0b", "To Do", "MEDIUM", "#6b7280", false, 0, 0, "Oct 24", 0, "Finance"),
        Task(1, "PROJECT::ZENITH", "Client presentation for Sustainable Skyscraper concept", "Prepare final deck and 3D renders for client walkthrough.", "Sarah Jenkins", "SJ", "#8b5cf6", "In Progress", "LOW", "#22c55e", false, 0, 0, "", 65, "Design"),
        Task(1, "PROJECT::HORIZON", "Environmental impact assessment for site Alpha", "Document environmental factors affecting the construction site.", "David L.", "DL", "#ef4444", "In Progress", "NORMAL", "#6b7280", false, 12, 0, "", 40, "Engineering"),
        Task(1, "SHARED SERVICES", "Budget reallocation for Q4 material procurement", "Review and reallocate construction material budget for Q4.", "John Doe", "JD", "#10b981", "Review", "CRITICAL REVIEW", "#ef4444", false, 2, 1, "", 90, "Finance"),
        Task(1, "PROJECT::HORIZON", "Fix load balancer config on staging env", "Load balancer is dropping connections intermittently.", "David L.", "DL", "#ef4444", "To Fix", "URGENT", "#ef4444", false, 3, 0, "Nov 02", 20, "Engineering"),
        Task(1, "PROJECT::NEO-LIFE", "Update API documentation for v2 endpoints", "All v2 endpoints must be documented in OpenAPI 3.0 format.", "Alex Sterling", "AS", "#3b82f6", "Done", "LOW", "#22c55e", true, 1, 2, "", 100, "Engineering"),
        Task(2, "PROJECT::NEO-NEXUS", "Set up Kubernetes cluster for microservices", "Deploy production-ready K8s cluster with autoscaling.", "Elena V.", "EV", "#06b6d4", "In Progress", "URGENT", "#ef4444", false, 5, 3, "Nov 10", 50, "DevOps"),
        Task(1, "", "dsfds", "dsfdsf", "Dylan", "DY", "#6b7280", "To Do", "URGENT", "#ef4444", false, 0, 0, "2026-05-15", 41, "")
    ];

    private static CreateCollaborationTaskCommand Task(
        int boardId, string project, string title, string description, string assignee,
        string assigneeAvatar, string assigneeColor, string status, string priority, string priorityColor,
        bool completed, int comments, int attachments, string dueDate, int progress, string department) =>
        new(boardId, project, title, description, assignee, assigneeAvatar, assigneeColor,
            status, priority, priorityColor, completed, comments, attachments, dueDate, progress, department);
}
