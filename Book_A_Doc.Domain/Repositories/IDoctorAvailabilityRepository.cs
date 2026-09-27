using Book_A_Doc.Domain.Models;

namespace Book_A_Doc.Domain.Repositories;

public interface IDoctorAvailabilityRepository
{
    Task<IEnumerable<DoctorAvailability>> GetByDoctorIdAsync(
        Guid doctorId,
        CancellationToken cancellationToken = default);

    Task<DoctorAvailability?> GetByIdAsync(
    Guid id,
    CancellationToken cancellationToken = default);

    Task<DoctorAvailability?> GetTrackedByIdAsync(
        Guid id,
        Guid doctorId,
        CancellationToken cancellationToken = default);

    Task<bool> HasOverlapAsync(
        Guid doctorId,
        DayOfWeek dayOfWeek,
        TimeOnly startTime,
        TimeOnly endTime,
        Guid? excludedId = null,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        DoctorAvailability availability,
        CancellationToken cancellationToken = default);
}