using Book_A_Doc.Domain.Models.Identity;
using Book_A_Doc.Domain.Repositories;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using Book_A_Doc.Infrastructre.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Book_A_Doc.Infrastructre.Repositories;

public class DoctorRepository(Book_A_Doc_Context context) : IDoctorRepository
{
    public async Task<Result> DeleteAsync(Guid Id, CancellationToken cancellationToken = default)
    {
        var doctor = await GetDoctorAsync(Id, cancellationToken);

        if (doctor is not null)
        {
            doctor.IsDeleted = true;

            await context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        return Result.Failure(UserErrors.DoctorNotFound);
    }

    public async Task<List<Doctor>> GetAllDoctorsAsync(CancellationToken cancellationToken = default)
    {
        var doctors = await context.Doctors.Where(d=>d.IsDeleted==false).Include(d => d.User).ToListAsync(cancellationToken);
        return doctors;
    }

    public async Task<Doctor?> GetDoctorAsync(Guid Id, CancellationToken cancellationToken = default)
    {
        var doctor = await context.Doctors.Where(d=>d.IsDeleted==false).Include(d => d.User).FirstOrDefaultAsync(d => d.UserId == Id, cancellationToken);
        return doctor is null? null : doctor;
    }
}
