using vantagePMO_platform.Meetings.Domain.Model.Aggregates;
using vantagePMO_platform.Meetings.Domain.Repositories;
using vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace vantagePMO_platform.Meetings.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
/// Implementación concreta de IMeetingRepository usando Entity Framework Core.
/// Hereda toda la lógica CRUD genérica de BaseRepository&lt;Meeting&gt;,
/// que ya tienes en Shared.Infrastructure.Persistence.EFC.Repositories.
/// </summary>
public class MeetingRepository(AppDbContext context)
    : BaseRepository<Meeting>(context), IMeetingRepository
{
}
