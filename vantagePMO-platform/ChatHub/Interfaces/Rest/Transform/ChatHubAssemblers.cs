using vantagePMO_platform.ChatHub.Domain.Model.Aggregates;
using vantagePMO_platform.ChatHub.Domain.Model.Commands;
using vantagePMO_platform.ChatHub.Domain.Model.ValueObjects;
using vantagePMO_platform.ChatHub.Interfaces.Rest.Resources;

namespace vantagePMO_platform.ChatHub.Interfaces.Rest.Transform;

public static class ChatUserResourceFromEntityAssembler
{
    public static ChatUserResource ToResourceFromEntity(ChatUser entity) =>
        new(entity.Id, entity.Name, entity.AvatarSeed, entity.Avatar, entity.Status, entity.Role);

    public static IEnumerable<ChatUserResource> ToResourcesFromEntities(IEnumerable<ChatUser> entities) =>
        entities.Select(ToResourceFromEntity);
}

public static class ChatResourceFromEntityAssembler
{
    public static ChatResource ToResourceFromEntity(Chat entity) =>
        new(entity.Id, entity.Name, entity.Type, entity.Description, entity.Members, entity.IsFavorited);

    public static IEnumerable<ChatResource> ToResourcesFromEntities(IEnumerable<Chat> entities) =>
        entities.Select(ToResourceFromEntity);
}

public static class ChatCommandFromResourceAssembler
{
    public static CreateChatCommand ToCreateCommandFromResource(CreateChatResource resource) =>
        new(resource.Id, resource.Name, resource.Type, resource.Description, resource.Members, resource.IsFavorited);

    public static PatchChatCommand ToPatchCommandFromResource(string id, PatchChatResource resource) =>
        new(id, resource.Name, resource.Description, resource.Members, resource.IsFavorited);
}

public static class ChatMessageResourceFromEntityAssembler
{
    public static ChatMessageResource ToResourceFromEntity(ChatMessage entity) =>
        new(
            entity.Id,
            entity.ChatId,
            entity.AuthorId,
            entity.Timestamp,
            entity.Text,
            entity.Attachments.Select(attachment =>
                new ChatAttachmentResource(attachment.Name, attachment.Icon, attachment.Type)),
            entity.Reactions.Select(reaction =>
                new ChatReactionResource(reaction.Emoji, reaction.Count)));

    public static IEnumerable<ChatMessageResource> ToResourcesFromEntities(IEnumerable<ChatMessage> entities) =>
        entities.Select(ToResourceFromEntity);
}

public static class ChatMessageCommandFromResourceAssembler
{
    public static CreateChatMessageCommand ToCreateCommandFromResource(CreateChatMessageResource resource) =>
        new(
            resource.Id,
            resource.ChatId,
            resource.AuthorId,
            resource.Timestamp,
            resource.Text,
            resource.Attachments?.Select(attachment =>
                new ChatAttachment
                {
                    Name = attachment.Name,
                    Icon = attachment.Icon,
                    Type = attachment.Type
                }),
            resource.Reactions?.Select(reaction =>
                new ChatReaction
                {
                    Emoji = reaction.Emoji,
                    Count = reaction.Count
                }));
}

public static class ChatPinnedAssetResourceFromEntityAssembler
{
    public static ChatPinnedAssetResource ToResourceFromEntity(ChatPinnedAsset entity) =>
        new(entity.Id, entity.ChatId, entity.Name, entity.Type, entity.Meta);

    public static IEnumerable<ChatPinnedAssetResource> ToResourcesFromEntities(IEnumerable<ChatPinnedAsset> entities) =>
        entities.Select(ToResourceFromEntity);
}

public static class ChatInsightResourceFromEntityAssembler
{
    public static ChatInsightResource ToResourceFromEntity(ChatInsight entity) =>
        new(
            entity.ChatId,
            entity.MeetingTag,
            entity.TimeAgo,
            entity.MeetingTitle,
            entity.Insights.Select(item => new InsightItemResource(item.Id, item.Type, item.Text)),
            entity.SentimentProductive,
            entity.SentimentText);

    public static IEnumerable<ChatInsightResource> ToResourcesFromEntities(IEnumerable<ChatInsight> entities) =>
        entities.Select(ToResourceFromEntity);
}
