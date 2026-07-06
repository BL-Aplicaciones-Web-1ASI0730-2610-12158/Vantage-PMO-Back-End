using vantagePMO_platform.Meetings.Domain.Model.Aggregates;
using vantagePMO_platform.Shared.Domain.Repositories;

namespace vantagePMO_platform.Meetings.Domain.Repositories;

/// <summary>
/// Contrato del repositorio de Meeting. Hereda las operaciones CRUD base
/// (AddAsync, FindByIdAsync, ListAsync, Update, Remove) desde IBaseRepository&lt;T&gt;,
/// que ya tienes definida en Shared.Domain.Repositories.
/// Aquí puedes agregar métodos específicos de Meeting si los necesitas
/// (por ejemplo: ListByOrganizerAsync, ListByStatusAsync, etc.).
/// </summary>
public interface IMeetingRepository : IBaseRepository<Meeting>
{
}
