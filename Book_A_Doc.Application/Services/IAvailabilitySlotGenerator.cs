namespace Book_A_Doc.Application.Services;

public interface IAvailabilitySlotGenerator
{
    Task GenerateAsync(
        DateOnly day,
        CancellationToken cancellationToken = default);


    Task CleanOldSlotsAsync(
        CancellationToken cancellationToken = default);
}
