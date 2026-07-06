using vantagePMO_platform.Schedule.Domain.Model.Commands;
using vantagePMO_platform.Schedule.Interfaces.Rest.Resources;

namespace vantagePMO_platform.Schedule.Interfaces.Rest.Transform;

/// <summary>
///     Assembler for converting create API resources to domain commands.
/// </summary>
public class CreateScheduleCommandFromResourceAssembler
{
    public static CreateScheduleCommand ToCommand(CreateScheduleResource resource)
    {
        return new CreateScheduleCommand(
            resource.UserId,
            resource.Date,
            resource.Time,
            resource.Duration,
            resource.Title,
            resource.Detail,
            resource.Type,
            resource.Active);
    }
}
