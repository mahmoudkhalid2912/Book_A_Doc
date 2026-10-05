namespace Book_A_Doc.Domain.Interfaces.Repositories;

public interface ISlotRepository
{
    Task<IEnumerable<AvailabilitySlot>> GetAvailableSlotsByDoctorAndDateAsync(
    Guid doctorId,
    DateOnly date,
    CancellationToken cancellationToken = default);

    Task<IEnumerable<AvailabilitySlot>> GetSlotsByDoctorAndDateRangeAsync(
        Guid doctorId,
        DateOnly startDate,
        DateOnly endDate,
        CancellationToken cancellationToken = default);

    Task<AvailabilitySlot?> GetByIdAsync(
       Guid id,
       CancellationToken cancellationToken = default);

    void Update(AvailabilitySlot slot);

}