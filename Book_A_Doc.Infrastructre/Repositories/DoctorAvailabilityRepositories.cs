using Book_A_Doc.Domain.Models;
using Book_A_Doc.Domain.Repositories;
using Book_A_Doc.Infrastructre.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Book_A_Doc.Infrastructure.Repositories;

public class DoctorAvailabilityRepository(
    Book_A_Doc_Context context)
    : IDoctorAvailabilityRepository
{
    private readonly Book_A_Doc_Context _context = context;

    public async Task<IEnumerable<DoctorAvailability>> GetByDoctorIdAsync(
        Guid doctorId,
        CancellationToken cancellationToken = default)
    {
        return await _context.DoctorAvailabilities
            .AsNoTracking()
            .Where(x =>
                x.DoctorId == doctorId &&
                x.IsActive&&x.IsDeleted==false)
            .OrderBy(x => x.DayOfWeek)
            .ThenBy(x => x.StartTime)
            .ToListAsync(cancellationToken);
    }

    public async Task<DoctorAvailability?> GetByIdAsync(
    Guid AvailabilityId,
    Guid DoctorId,
    CancellationToken cancellationToken = default)
    {
        var availability = await _context.DoctorAvailabilities
            .AsNoTracking()
            .Include(x => x.Doctor)
            .FirstOrDefaultAsync(
                x => x.Id == AvailabilityId && x.DoctorId == DoctorId,
                cancellationToken);
        return availability;
    }
    

    public async Task<DoctorAvailability?> GetTrackedByIdAsync(
        Guid AvailabilityId,
        Guid DoctorId,
        CancellationToken cancellationToken = default)
    {
        return await _context.DoctorAvailabilities
            .FirstOrDefaultAsync(
                x =>
                    x.Id == AvailabilityId &&
                    x.DoctorId == DoctorId,
                cancellationToken);
    }

    public async Task<bool> HasOverlapAsync(
        Guid doctorId,
        DayOfWeek dayOfWeek,
        TimeOnly startTime,
        TimeOnly endTime,
        Guid? excludedId = null,
        CancellationToken cancellationToken = default)
    {
        return await _context.DoctorAvailabilities
            .AsNoTracking()
            .AnyAsync(
                x =>
                    x.DoctorId == doctorId &&
                    x.DayOfWeek == dayOfWeek &&
                    x.IsActive &&
                    (!excludedId.HasValue || x.Id != excludedId.Value) &&
                    x.StartTime < endTime &&
                    startTime < x.EndTime,
                cancellationToken);
    }

    public async Task AddAsync(
        DoctorAvailability availability,
        CancellationToken cancellationToken = default)
    {
        await _context.DoctorAvailabilities.AddAsync(
            availability,
            cancellationToken);
    }
}