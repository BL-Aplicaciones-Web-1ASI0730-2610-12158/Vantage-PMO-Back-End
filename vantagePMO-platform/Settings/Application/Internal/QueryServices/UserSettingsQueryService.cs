using vantagePMO_platform.Settings.Application.QueryServices;
using vantagePMO_platform.Settings.Domain.Model.Aggregates;
using vantagePMO_platform.Settings.Domain.Model.Queries;
using vantagePMO_platform.Settings.Domain.Repositories;

namespace vantagePMO_platform.Settings.Application.Internal.QueryServices;

public class UserSettingsQueryService(IUserSettingsRepository userSettingsRepository) : IUserSettingsQueryService
{
    public async Task<UserSettings?> Handle(
        GetUserSettingsQuery query,
        CancellationToken cancellationToken = default)
    {
        return await userSettingsRepository.GetSingletonAsync(cancellationToken);
    }
}
