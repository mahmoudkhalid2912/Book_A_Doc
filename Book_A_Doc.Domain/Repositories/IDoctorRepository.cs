using Book_A_Doc.Domain.Models.Identity;

namespace Book_A_Doc.Domain.Repositories;

public interface IDoctorRepository
{
    Task<List<Doctor>> GetAllDoctorsAsync(CancellationToken cancellationToken = default);

    Task<Doctor?> GetDoctorAsync(Guid Id, CancellationToken cancellationToken = default);
}
