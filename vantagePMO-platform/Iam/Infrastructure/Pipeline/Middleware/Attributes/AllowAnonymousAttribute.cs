namespace vantagePMO_platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;

/**
 * This attribute is used to decorate controllers and actions that do not require authorization.
 * It skips authorization if the action is decorated with [AllowAnonymous] attribute.
 */
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AllowAnonymousAttribute : Attribute
{
}