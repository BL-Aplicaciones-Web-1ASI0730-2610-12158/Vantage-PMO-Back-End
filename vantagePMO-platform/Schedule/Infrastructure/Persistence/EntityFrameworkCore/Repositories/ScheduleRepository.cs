using vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Microsoft.EntityFrameworkCore;
using vantagePMO_platform.Schedule.Domain.Model.Aggregates;
using vantagePMO_platform.Schedule.Domain.Repositories;
using vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore;

namespace vantagePMO_platform.Schedule.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class ScheduleRepository : IScheduleRepository
{
    private readonly AppDbContext _context;

    public ScheduleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ScheduleEntry?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.ScheduleEntries
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<ScheduleEntry>> FindByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        return await _context.ScheduleEntries
            .Where(s => s.UserId == userId)
            .OrderBy(s => s.Date)
            .ThenBy(s => s.Time)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ScheduleEntry>> FindByUserIdAndDateRangeAsync(int userId, DateTime startDate,
        DateTime endDate, CancellationToken cancellationToken = default)
    {
        return await _context.ScheduleEntries
            .Where(s => s.UserId == userId && s.Date >= startDate && s.Date <= endDate && s.Active)
            .OrderBy(s => s.Date)
            .ThenBy(s => s.Time)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ScheduleEntry entity, CancellationToken cancellationToken = default)
    {
        await _context.ScheduleEntries.AddAsync(entity, cancellationToken);
    }

    public void Update(ScheduleEntry entity)
    {
        _context.ScheduleEntries.Update(entity);
    }

    public void Remove(ScheduleEntry entity)
    {
        _context.ScheduleEntries.Remove(entity);
    }

    public async Task<IEnumerable<ScheduleEntry>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await _context.ScheduleEntries.ToListAsync(cancellationToken);
    }

    public async Task<ScheduleEntry?> FindAsync(int id, CancellationToken cancellationToken = default)
    {
        return await FindByIdAsync(id, cancellationToken);
    }
}
