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
            doctor.User.IsDeleted = true;

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
        var doctor = await  context.Doctors
        .Include(d => d.User)
        .Where(d => !d.IsDeleted && !d.User.IsDeleted)
        .FirstOrDefaultAsync(d => d.UserId == Id, cancellationToken);

        return doctor;
    }

    public async Task<Doctor?> GetDoctorWithAvailabilityAsync(
    Guid doctorId,
    DateOnly date,
    CancellationToken cancellationToken = default)
    {
       
        return await context.Doctors
            .AsNoTracking()
            .Where(d => d.UserId == doctorId)
            .Where(d => context.DoctorAvailabilities
                .Any(a => a.DoctorId == d.UserId
                       && a.DayOfWeek == date.DayOfWeek
                       && a.IsActive))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Result> UpdateAsync(Guid id, string? fullName, string? specialty, string? description, byte? yearsOfExperience, decimal? sessionPrice, DateOnly? birthDate, string? phoneNumber, CancellationToken cancellationToken = default)
    {
        var doctorUpdated = await context.Doctors
            .Where(d => d.UserId == id && !d.IsDeleted)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(d => d.FullName, d => string.IsNullOrWhiteSpace(fullName) ? d.FullName : fullName)
                .SetProperty(d => d.Specialty, d => string.IsNullOrWhiteSpace(specialty) ? d.Specialty : specialty)
                .SetProperty(d => d.Description, d => string.IsNullOrWhiteSpace(description) ? d.Description : description)
                .SetProperty(d => d.YearsOfExperience, d => yearsOfExperience ?? d.YearsOfExperience)
                .SetProperty(d => d.SessionPrice, d => sessionPrice ?? d.SessionPrice), cancellationToken);

        if (doctorUpdated == 0)
            return Result.Failure(UserErrors.DoctorNotFound);

        await context.Users
            .Where(u => u.Id == id)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(u => u.FullName, u => string.IsNullOrWhiteSpace(fullName) ? u.FullName : fullName)
                .SetProperty(u => u.BirthDate, u => birthDate ?? u.BirthDate)
                .SetProperty(u => u.PhoneNumber, u => string.IsNullOrWhiteSpace(phoneNumber) ? u.PhoneNumber : phoneNumber), cancellationToken);

        return Result.Success();
    }
}
