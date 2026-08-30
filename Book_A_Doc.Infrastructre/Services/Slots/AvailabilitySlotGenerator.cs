using Book_A_Doc.Application.Services;
using Book_A_Doc.Infrastructre.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Book_A_Doc.Infrastructre.Services;

public class AvailabilitySlotGenerator(
    Book_A_Doc_Context context) : IAvailabilitySlotGenerator
{
    private readonly Book_A_Doc_Context _context = context;

    public async Task GenerateAsync(
        DateOnly day,
        CancellationToken cancellationToken = default)
    {
        

        var isHoliday = await _context.Holidays
            .AsNoTracking()
            .AnyAsync(
                x => x.Date == day &&
                     x.IsActive,
                cancellationToken);

        if (isHoliday)
            return;


        

        var availabilities = await _context.DoctorAvailabilities
            .AsNoTracking()
            .Join(
                _context.Doctors,
                availability => availability.DoctorId,
                doctor => doctor.UserId,
                (availability, doctor) => new
                {
                    Availability = availability,
                    DoctorIsDeleted = doctor.IsDeleted
                })
            .Where(x =>
                x.Availability.IsActive &&
                x.Availability.DayOfWeek == day.DayOfWeek &&
                !x.DoctorIsDeleted)
            .Select(x => x.Availability)
            .ToListAsync(cancellationToken);

        if (availabilities.Count == 0)
            return;


     

        var exceptionDoctorIds = await _context.DoctorExceptions
            .AsNoTracking()
            .Where(x => x.Date == day)
            .Select(x => x.DoctorId)
            .ToHashSetAsync(cancellationToken);


       

        var existingSlots = await _context.AvailabilitySlots
            .AsNoTracking()
            .Where(x => x.Date == day)
            .Select(x => new
            {
                x.DoctorId,
                x.StartTime,
                x.EndTime
            })
            .ToListAsync(cancellationToken);

        var existingSlotKeys = existingSlots
            .Select(x => (
                x.DoctorId,
                x.StartTime,
                x.EndTime))
            .ToHashSet();


       

        var newSlots = new List<AvailabilitySlot>();

        foreach (var availability in availabilities)
        {
          
            if (exceptionDoctorIds.Contains(
                    availability.DoctorId))
            {
                continue;
            }

            var currentTime = availability.StartTime;

            while (currentTime.AddMinutes(
                       availability.SlotDurationInMinutes)
                   <= availability.EndTime)
            {
                var endTime = currentTime.AddMinutes(
                    availability.SlotDurationInMinutes);

                var key = (
                    availability.DoctorId,
                    currentTime,
                    endTime);

                if (existingSlotKeys.Add(key))
                {
                    newSlots.Add(new AvailabilitySlot
                    {
                        Id = Guid.NewGuid(),
                        DoctorId = availability.DoctorId,
                        Date = day,
                        StartTime = currentTime,
                        EndTime = endTime
                    });
                }

                currentTime = endTime;
            }
        }


        // -----------------------------------------
        // 6. Nothing to save
        // -----------------------------------------

        if (newSlots.Count == 0)
            return;


        // -----------------------------------------
        // 7. Save
        // -----------------------------------------

        await _context.AvailabilitySlots
            .AddRangeAsync(
                newSlots,
                cancellationToken);

        await _context.SaveChangesAsync(
            cancellationToken);
    }


    public async Task CleanOldSlotsAsync(
        CancellationToken cancellationToken = default)
    {
        

        var today = DateOnly.FromDateTime(
            DateTime.Today);

        var cutoffDate = today.AddDays(-30);

        await _context.AvailabilitySlots
            .Where(x => x.Date < cutoffDate)
            .ExecuteDeleteAsync(
                cancellationToken);
    }
}