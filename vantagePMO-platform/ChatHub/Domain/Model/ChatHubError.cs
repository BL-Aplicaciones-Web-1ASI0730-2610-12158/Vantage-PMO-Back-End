namespace vantagePMO_platform.ChatHub.Domain.Model;

public enum ChatHubError
{
    ChatNotFound,
    InvalidChatData,
    InvalidMessageData,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
