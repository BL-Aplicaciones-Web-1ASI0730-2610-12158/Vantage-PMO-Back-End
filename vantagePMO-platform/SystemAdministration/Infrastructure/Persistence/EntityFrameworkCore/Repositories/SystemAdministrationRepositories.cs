using Microsoft.EntityFrameworkCore;
using vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using vantagePMO_platform.SystemAdministration.Domain.Model.Aggregates;
using vantagePMO_platform.SystemAdministration.Domain.Repositories;

namespace vantagePMO_platform.SystemAdministration.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class BrandingRepository(AppDbContext context)
    : BaseRepository<Branding>(context), IBrandingRepository
{
    public async Task<Branding?> GetSingletonAsync(CancellationToken cancellationToken = default) =>
        await context.Set<Branding>()
            .FirstOrDefaultAsync(entity => entity.Id == Branding.SingletonId, cancellationToken);

    public async Task<bool> AnyAsync(CancellationToken cancellationToken = default) =>
        await context.Set<Branding>().AnyAsync(cancellationToken);
}

public class SubscriptionRepository(AppDbContext context)
    : BaseRepository<Subscription>(context), ISubscriptionRepository
{
    public async Task<Subscription?> GetSingletonAsync(CancellationToken cancellationToken = default) =>
        await context.Set<Subscription>()
            .FirstOrDefaultAsync(entity => entity.Id == Subscription.SingletonId, cancellationToken);

    public async Task<bool> AnyAsync(CancellationToken cancellationToken = default) =>
        await context.Set<Subscription>().AnyAsync(cancellationToken);
}

public class AdminPolicyRepository(AppDbContext context)
    : BaseRepository<AdminPolicy>(context), IAdminPolicyRepository
{
    public async Task<AdminPolicy?> GetSingletonAsync(CancellationToken cancellationToken = default) =>
        await context.Set<AdminPolicy>()
            .FirstOrDefaultAsync(entity => entity.Id == AdminPolicy.SingletonId, cancellationToken);

    public async Task<bool> AnyAsync(CancellationToken cancellationToken = default) =>
        await context.Set<AdminPolicy>().AnyAsync(cancellationToken);
}

public class SystemSettingsRepository(AppDbContext context)
    : BaseRepository<Domain.Model.Aggregates.SystemSettings>(context), ISystemSettingsRepository
{
    public async Task<Domain.Model.Aggregates.SystemSettings?> GetSingletonAsync(CancellationToken cancellationToken = default) =>
        await context.Set<Domain.Model.Aggregates.SystemSettings>()
            .FirstOrDefaultAsync(entity => entity.Id == Domain.Model.Aggregates.SystemSettings.SingletonId, cancellationToken);

    public async Task<bool> AnyAsync(CancellationToken cancellationToken = default) =>
        await context.Set<Domain.Model.Aggregates.SystemSettings>().AnyAsync(cancellationToken);
}

public class LoginAttemptRepository(AppDbContext context) : ILoginAttemptRepository
{
    public async Task<IReadOnlyList<LoginAttempt>> ListOrderedAsync(CancellationToken cancellationToken = default) =>
        await context.Set<LoginAttempt>()
            .OrderBy(attempt => attempt.Id)
            .ToListAsync(cancellationToken);

    public async Task AddRangeAsync(IEnumerable<LoginAttempt> attempts, CancellationToken cancellationToken = default) =>
        await context.Set<LoginAttempt>().AddRangeAsync(attempts, cancellationToken);

    public async Task<bool> AnyAsync(CancellationToken cancellationToken = default) =>
        await context.Set<LoginAttempt>().AnyAsync(cancellationToken);
}
