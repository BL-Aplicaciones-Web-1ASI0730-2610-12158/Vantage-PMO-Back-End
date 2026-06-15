using vantagePMO_platform.Iam.Application.Internal.OutboundServices;
using vantagePMO_platform.Iam.Application.QueryServices;
using vantagePMO_platform.Iam.Domain.Model.Queries;
using vantagePMO_platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;

namespace vantagePMO_platform.Iam.Infrastructure.Pipeline.Middleware.Components;

/**
 * RequestAuthorizationMiddleware is a custom middleware.
 * This middleware is used to authorize requests.
 * It validates a token is included in the request header and that the token is valid.
 * If the token is valid then it sets the user in HttpContext.Items["User"].
 */
public class RequestAuthorizationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(
        HttpContext context,
        IUserQueryService userQueryService,
        ITokenService tokenService)
    {
        var endpoint = context.GetEndpoint();
        var allowAnonymous = endpoint?.Metadata.Any(m => m is AllowAnonymousAttribute) == true;

        if (allowAnonymous)
        {
            await next(context);
            return;
        }

        var token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(' ').Last();

        if (token is null)
            throw new UnauthorizedAccessException("Null or invalid token");

        var userId = await tokenService.ValidateToken(token);

        if (userId is null)
            throw new UnauthorizedAccessException("Invalid token");

        var user = await userQueryService.Handle(
            new GetUserByIdQuery(userId.Value),
            context.RequestAborted);

        context.Items["User"] = user;
        await next(context);
    }
}
