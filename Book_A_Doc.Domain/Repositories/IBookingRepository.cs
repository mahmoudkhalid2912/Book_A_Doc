namespace Book_A_Doc.Domain.Interfaces.Repositories;

public interface IBookingRepository
{
    Task AddAsync(
        Booking booking,
        CancellationToken cancellationToken = default);

    Task<Booking?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<List<Booking>> GetByPatientAsync(
        Guid patientId,
        CancellationToken cancellationToken = default);

    void Update(Booking booking);
}
