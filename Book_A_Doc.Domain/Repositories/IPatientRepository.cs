using Book_A_Doc.Domain.Models.Identity;

namespace Book_A_Doc.Domain.Repositories;

public interface IPatientRepository
{
    Task AddAsync(Patient patient, CancellationToken cancellationToken);

    Task<bool> ExistsAsync(Guid userId, CancellationToken cancellationToken = default);
}
