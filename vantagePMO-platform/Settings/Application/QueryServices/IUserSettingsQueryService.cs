using vantagePMO_platform.Settings.Domain.Model.Aggregates;
using vantagePMO_platform.Settings.Domain.Model.Queries;

namespace vantagePMO_platform.Settings.Application.QueryServices;

public interface IUserSettingsQueryService
{
    Task<UserSettings?> Handle(GetUserSettingsQuery query, CancellationToken cancellationToken = default);
}
