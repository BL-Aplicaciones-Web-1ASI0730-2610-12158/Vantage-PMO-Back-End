using vantagePMO_platform.Schedule.Domain.Model.Commands;
using vantagePMO_platform.Schedule.Interfaces.Rest.Resources;

namespace vantagePMO_platform.Schedule.Interfaces.Rest.Transform;

/// <summary>
///     Assembler for converting update API resources to domain commands.
/// </summary>
public class UpdateScheduleCommandFromResourceAssembler
{
    public static UpdateScheduleCommand ToCommand(int id, UpdateScheduleResource resource)
    {
        return new UpdateScheduleCommand(
            id,
            resource.Date,
            resource.Time,
            resource.Duration,
            resource.Title,
            resource.Detail,
            resource.Type,
            resource.Active);
    }
}
