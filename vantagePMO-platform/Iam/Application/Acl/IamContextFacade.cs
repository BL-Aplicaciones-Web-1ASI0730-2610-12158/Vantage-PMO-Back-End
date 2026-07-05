using vantagePMO_platform.Iam.Application.CommandServices;
using vantagePMO_platform.Iam.Application.QueryServices;
using vantagePMO_platform.Iam.Domain.Model.Commands;
using vantagePMO_platform.Iam.Domain.Model.Queries;
using vantagePMO_platform.Iam.Interfaces.Acl;

namespace vantagePMO_platform.Iam.Application.Acl;

public class IamContextFacade(IUserCommandService userCommandService, IUserQueryService userQueryService)
    : IIamContextFacade
{
    public async Task<int> CreateUser(string username, string password, CancellationToken cancellationToken)
    {
        var email = username.Contains('@') ? username : $"{username}@users.local";
        var signUpCommand = new SignUpCommand(
            username,
            password,
            username,
            email,
            string.Empty,
            new DateOnly(2000, 1, 1));

        var signUpResult = await userCommandService.Handle(signUpCommand, cancellationToken);
        return signUpResult.IsSuccess ? signUpResult.Value : 0;
    }

    public async Task<int> FetchUserIdByUsername(string username, CancellationToken cancellationToken)
    {
        var getUserByUsernameQuery = new GetUserByUsernameQuery(username);
        var result = await userQueryService.Handle(getUserByUsernameQuery, cancellationToken);
        return result?.Id ?? 0;
    }

    public async Task<string> FetchUsernameByUserId(int userId, CancellationToken cancellationToken)
    {
        var getUserByIdQuery = new GetUserByIdQuery(userId);
        var result = await userQueryService.Handle(getUserByIdQuery, cancellationToken);
        return result?.Username ?? string.Empty;
    }
}
