using vantagePMO_platform.ChatHub.Domain.Model.Aggregates;
using vantagePMO_platform.ChatHub.Domain.Model.Commands;
using vantagePMO_platform.ChatHub.Domain.Model.ValueObjects;
using vantagePMO_platform.ChatHub.Domain.Repositories;
using VantagePMO_platform.Shared.Domain.Repositories;

namespace vantagePMO_platform.ChatHub.Application.Internal.CommandServices;

/// <summary>
///     Seeds Chat Hub data when tables are empty (matches front-end CHAT_SEED).
/// </summary>
public class ChatHubSampleDataSeeder(
    IChatUserRepository chatUserRepository,
    IChatRepository chatRepository,
    IChatMessageRepository chatMessageRepository,
    IChatPinnedAssetRepository chatPinnedAssetRepository,
    IChatInsightRepository chatInsightRepository,
    IUnitOfWork unitOfWork)
{
    public async Task SeedIfEmptyAsync(CancellationToken cancellationToken = default)
    {
        var seeded = false;

        if (!await chatUserRepository.AnyAsync(cancellationToken))
        {
            await chatUserRepository.AddRangeAsync(
            [
                new ChatUser("alex", "Alex Sterling", "Alex", null, "online", "Precision Lead"),
                new ChatUser("elena", "Elena L.", "Elena", null, "online", "Senior Analyst"),
                new ChatUser("sarah", "Sarah Jenkins", "Sarah", null, "online", "CTO"),
                new ChatUser("david", "David Chen", "David", null, "offline", "Risk Manager"),
                new ChatUser("anna", "Anna K.", "Anna", null, "online", "Budget Analyst"),
                new ChatUser("marcus", "Marcus Vane", "Marcus", null, "online", "Senior Director")
            ],
                cancellationToken);
            seeded = true;
        }

        if (!await chatRepository.AnyAsync(cancellationToken))
        {
            await chatRepository.AddRangeAsync(
            [
                new Chat(new CreateChatCommand(
                    "pmo-strategic-alignment",
                    "pmo-strategic-alignment",
                    "channel",
                    "Strategic discussions for Q3 planning and executive alignment.",
                    ["alex", "elena", "sarah", "david", "marcus"],
                    false)),
                new Chat(new CreateChatCommand(
                    "budget-steering-comm",
                    "budget-steering-comm",
                    "channel",
                    "Discussions for budget steering committee.",
                    ["alex", "anna", "marcus"],
                    false)),
                new Chat(new CreateChatCommand(
                    "risk-mitigation-log",
                    "risk-mitigation-log",
                    "channel",
                    "Logging and mitigation strategies for project risks.",
                    ["alex", "david"],
                    false)),
                new Chat(new CreateChatCommand(
                    "sarah-dm",
                    "Sarah Jenkins",
                    "dm",
                    null,
                    ["alex", "sarah"],
                    false)),
                new Chat(new CreateChatCommand(
                    "david-dm",
                    "David Chen",
                    "dm",
                    null,
                    ["alex", "david"],
                    false))
            ],
                cancellationToken);
            seeded = true;
        }

        if (!await chatMessageRepository.AnyAsync(cancellationToken))
        {
            await chatMessageRepository.AddRangeAsync(
            [
                new ChatMessage(new CreateChatMessageCommand(
                    "msg1",
                    "pmo-strategic-alignment",
                    "alex",
                    "10:30 AM",
                    "Team, great progress on the Q3 planning. Let's ensure all strategic alignment documents are finalized by end of day.",
                    [],
                    [new ChatReaction { Emoji = "👍", Count = 3 }])),
                new ChatMessage(new CreateChatMessageCommand(
                    "msg2",
                    "pmo-strategic-alignment",
                    "elena",
                    "10:35 AM",
                    "Agreed, Alex. I'm currently reviewing the resource_reallocation_impact.pdf. It looks solid.",
                    [new ChatAttachment { Name = "resource_reallocation_impact.pdf", Icon = "pdf", Type = "pdf" }],
                    [new ChatReaction { Emoji = "🚀", Count = 1 }])),
                new ChatMessage(new CreateChatMessageCommand(
                    "msg3",
                    "pmo-strategic-alignment",
                    "alex",
                    "10:40 AM",
                    "Excellent, Elena. Once you're done, please upload the final version to the shared drive.",
                    [],
                    [])),
                new ChatMessage(new CreateChatMessageCommand(
                    "msg4",
                    "sarah-dm",
                    "sarah",
                    "11:00 AM",
                    "Hi Alex, can we discuss the Q3 budget adjustments later today?",
                    [],
                    [])),
                new ChatMessage(new CreateChatMessageCommand(
                    "msg5",
                    "sarah-dm",
                    "alex",
                    "11:05 AM",
                    "Sure, Sarah. I'm free after 2 PM. Let me know what time works best for you.",
                    [],
                    []))
            ],
                cancellationToken);
            seeded = true;
        }

        if (!await chatPinnedAssetRepository.AnyAsync(cancellationToken))
        {
            await chatPinnedAssetRepository.AddRangeAsync(
            [
                new ChatPinnedAsset("pmo-strategic-alignment", "master_pmo_roadmap_v2.xls", "excel", "Modified 2h ago"),
                new ChatPinnedAsset("pmo-strategic-alignment", "strategic_alignment_brief.pdf", "pdf", "Pinned by Alex")
            ],
                cancellationToken);
            seeded = true;
        }

        if (!await chatInsightRepository.AnyAsync(cancellationToken))
        {
            await chatInsightRepository.AddRangeAsync(
            [
                new ChatInsight(
                    "pmo-strategic-alignment",
                    "RECENT MEETING",
                    "45m ago",
                    "Q3 Planning Sync: Steering Committee",
                    [
                        new InsightItem { Id = 1, Type = "Decision", Text = "Approved 12% budget shift to infrastructure phase." },
                        new InsightItem { Id = 2, Type = "Action", Text = "Elena to finalize Gantt by EOD Friday." }
                    ],
                    90,
                    "Productive")
            ],
                cancellationToken);
            seeded = true;
        }

        if (seeded)
            await unitOfWork.CompleteAsync(cancellationToken);
    }
}
