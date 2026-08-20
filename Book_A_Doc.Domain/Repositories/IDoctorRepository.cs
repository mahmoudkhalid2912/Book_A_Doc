using Book_A_Doc.Domain.Models.Identity;
using Book_A_Doc.Domain.ResultPattern;

namespace Book_A_Doc.Domain.Repositories;

public interface IDoctorRepository
{
    Task<List<Doctor>> GetAllDoctorsAsync(CancellationToken cancellationToken = default);

    Task<Doctor?> GetDoctorAsync(Guid Id, CancellationToken cancellationToken = default);

    Task<Result> DeleteAsync(Guid Id, CancellationToken cancellationToken = default);
}
