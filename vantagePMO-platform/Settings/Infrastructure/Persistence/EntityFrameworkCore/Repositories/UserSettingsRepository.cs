using Microsoft.EntityFrameworkCore;
using vantagePMO_platform.Settings.Domain.Model.Aggregates;
using vantagePMO_platform.Settings.Domain.Repositories;
using vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace vantagePMO_platform.Settings.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class UserSettingsRepository(AppDbContext context)
    : BaseRepository<UserSettings>(context), IUserSettingsRepository
{
    public async Task<UserSettings?> GetSingletonAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<UserSettings>()
            .FirstOrDefaultAsync(settings => settings.Id == UserSettings.SingletonId, cancellationToken);
    }

    public async Task<bool> AnyAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<UserSettings>().AnyAsync(cancellationToken);
    }
}
