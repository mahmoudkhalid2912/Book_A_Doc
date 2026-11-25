using Book_A_Doc.Domain.Interfaces.Repositories;
using Book_A_Doc.Infrastructre.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Book_A_Doc.Infrastructure.Repositories;

public class BookingRepository(Book_A_Doc_Context context) : IBookingRepository
{
    public async Task AddAsync(
        Booking booking,
        CancellationToken cancellationToken = default)
    {
        await context.Bookings.AddAsync(booking, cancellationToken);
    }

    public async Task<Booking?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await context.Bookings
            .Include(b => b.AvailabilitySlot)
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    }

    public async Task<List<Booking>> GetByPatientAsync(
        Guid patientId,
        CancellationToken cancellationToken = default)
    {
        return await context.Bookings
            .AsNoTracking()
            .Include(b => b.AvailabilitySlot)
            .Where(b => b.PatientId == patientId)
            .OrderByDescending(b => b.AvailabilitySlot.Date)
            .ThenByDescending(b => b.AvailabilitySlot.StartTime)
            .ToListAsync(cancellationToken);
    }

    public void Update(Booking booking)
    {
        context.Bookings.Update(booking);
    }
}
