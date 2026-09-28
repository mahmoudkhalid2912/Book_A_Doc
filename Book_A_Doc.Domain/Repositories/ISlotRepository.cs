namespace Book_A_Doc.Domain.Interfaces.Repositories;

public interface ISlotRepository
{
    Task<IEnumerable<AvailabilitySlot>> GetAvailableSlotsByDoctorAndDateAsync(
    Guid doctorId,
    DateOnly date,
    CancellationToken cancellationToken = default);

}