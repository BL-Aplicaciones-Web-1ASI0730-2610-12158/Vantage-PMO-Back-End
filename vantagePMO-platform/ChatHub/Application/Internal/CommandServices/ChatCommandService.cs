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

public class ChatCommandService(
    IChatRepository chatRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer,
    ILogger<ChatCommandService> logger) : IChatCommandService
{
    public async Task<Result<Chat>> CreateChat(CreateChatCommand command, CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(command.Id) || string.IsNullOrWhiteSpace(command.Name))
            {
                return Result<Chat>.Failure(
                    ChatHubError.InvalidChatData,
                    localizer["ChatHubError.InvalidChatData"]);
            }

            var existing = await chatRepository.FindByIdAsync(command.Id.Trim(), cancellationToken);
            if (existing is not null)
            {
                return Result<Chat>.Failure(
                    ChatHubError.InvalidChatData,
                    localizer["ChatHubError.InvalidChatData"]);
            }

            var chat = new Chat(command);
            await chatRepository.AddAsync(chat, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Chat>.Success(chat);
        }
        catch (OperationCanceledException)
        {
            return Result<Chat>.Failure(
                ChatHubError.OperationCancelled,
                localizer["ChatHubError.OperationCancelled"]);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database error creating chat");
            return Result<Chat>.Failure(
                ChatHubError.DatabaseError,
                localizer["ChatHubError.DatabaseError"]);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error creating chat");
            return Result<Chat>.Failure(
                ChatHubError.InternalServerError,
                localizer["ChatHubError.InternalServerError"]);
        }
    }

    public async Task<Result<Chat>> PatchChat(PatchChatCommand command, CancellationToken cancellationToken = default)
    {
        try
        {
            var chat = await chatRepository.FindByIdAsync(command.Id, cancellationToken);
            if (chat is null)
            {
                return Result<Chat>.Failure(
                    ChatHubError.ChatNotFound,
                    localizer["ChatHubError.ChatNotFound"]);
            }

            chat.Patch(command);
            chatRepository.Update(chat);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Chat>.Success(chat);
        }
        catch (OperationCanceledException)
        {
            return Result<Chat>.Failure(
                ChatHubError.OperationCancelled,
                localizer["ChatHubError.OperationCancelled"]);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database error patching chat {ChatId}", command.Id);
            return Result<Chat>.Failure(
                ChatHubError.DatabaseError,
                localizer["ChatHubError.DatabaseError"]);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error patching chat {ChatId}", command.Id);
            return Result<Chat>.Failure(
                ChatHubError.InternalServerError,
                localizer["ChatHubError.InternalServerError"]);
        }
    }
}
