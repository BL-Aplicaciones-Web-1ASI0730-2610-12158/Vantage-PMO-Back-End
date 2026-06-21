using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using vantagePMO_platform.ChatHub.Application.CommandServices;
using vantagePMO_platform.ChatHub.Domain.Model;
using vantagePMO_platform.ChatHub.Domain.Model.Aggregates;
using vantagePMO_platform.ChatHub.Domain.Model.Commands;
using vantagePMO_platform.ChatHub.Domain.Repositories;
using VantagePMO_platform.Shared.Application.Model;
using VantagePMO_platform.Shared.Domain.Repositories;
using VantagePMO_platform.Shared.Resources.Errors;

namespace vantagePMO_platform.ChatHub.Application.Internal.CommandServices;

public class ChatMessageCommandService(
    IChatMessageRepository chatMessageRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer,
    ILogger<ChatMessageCommandService> logger) : IChatMessageCommandService
{
    public async Task<Result<ChatMessage>> CreateMessage(
        CreateChatMessageCommand command,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(command.Id)
                || string.IsNullOrWhiteSpace(command.ChatId)
                || string.IsNullOrWhiteSpace(command.AuthorId))
            {
                return Result<ChatMessage>.Failure(
                    ChatHubError.InvalidMessageData,
                    localizer["ChatHubError.InvalidMessageData"]);
            }

            var message = new ChatMessage(command);
            await chatMessageRepository.AddAsync(message, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<ChatMessage>.Success(message);
        }
        catch (OperationCanceledException)
        {
            return Result<ChatMessage>.Failure(
                ChatHubError.OperationCancelled,
                localizer["ChatHubError.OperationCancelled"]);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database error creating chat message");
            return Result<ChatMessage>.Failure(
                ChatHubError.DatabaseError,
                localizer["ChatHubError.DatabaseError"]);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error creating chat message");
            return Result<ChatMessage>.Failure(
                ChatHubError.InternalServerError,
                localizer["ChatHubError.InternalServerError"]);
        }
    }
}
