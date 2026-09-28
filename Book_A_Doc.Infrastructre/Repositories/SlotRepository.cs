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
}