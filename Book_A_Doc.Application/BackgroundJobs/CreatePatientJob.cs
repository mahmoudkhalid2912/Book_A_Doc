using Book_A_Doc.Domain.Models;
using Book_A_Doc.Domain.Models.Identity;
using Book_A_Doc.Domain.Repositories;

namespace Book_A_Doc.Application.BackgroundJobs;

public class CreatePatientJob(
    IPatientRepository patientRepository)
{
    public async Task ExecuteAsync(Guid userId)
    {
        var patient = new Patient
        {
            UserId = userId,
            CreatedOn = DateTime.UtcNow
        };

        await patientRepository.AddAsync(
            patient,
            CancellationToken.None);
    }
}