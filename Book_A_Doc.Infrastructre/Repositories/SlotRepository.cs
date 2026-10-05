using Book_A_Doc.Domain.Interfaces.Repositories;
using Book_A_Doc.Infrastructre.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Book_A_Doc.Infrastructure.Repositories;

public class SlotRepository : ISlotRepository
{
    private readonly Book_A_Doc_Context _context;

    public SlotRepository(Book_A_Doc_Context context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AvailabilitySlot>> GetAvailableSlotsByDoctorAndDateAsync(
        Guid doctorId,
        DateOnly date,
        CancellationToken cancellationToken = default)
    {
        return await _context.AvailabilitySlots
            .AsNoTracking()
            .Where(s => s.DoctorId == doctorId
                     && s.Date == date
                     && s.IsActive)
            .OrderBy(s => s.StartTime)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<AvailabilitySlot>> GetSlotsByDoctorAndDateRangeAsync(
        Guid doctorId,
        DateOnly startDate,
        DateOnly endDate,
        CancellationToken cancellationToken = default)
    {
        return await _context.AvailabilitySlots
            .AsNoTracking()
            .Include(s => s.Booking)          
            .Where(s => s.DoctorId == doctorId
                     && s.Date >= startDate
                     && s.Date <= endDate)
            .OrderBy(s => s.Date)
            .ThenBy(s => s.StartTime)
            .ToListAsync(cancellationToken);
    }

    public async Task<AvailabilitySlot?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.AvailabilitySlots
            .Include(s => s.Booking)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public void Update(AvailabilitySlot slot)
    {
        _context.AvailabilitySlots.Update(slot);
    }

}