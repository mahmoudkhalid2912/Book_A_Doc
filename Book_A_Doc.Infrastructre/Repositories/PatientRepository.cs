using Book_A_Doc.Domain.Models.Identity;
using Book_A_Doc.Domain.Repositories;
using Book_A_Doc.Infrastructre.Persistence;

namespace Book_A_Doc.Infrastructre.Repositories;

public class PatientRepository(Book_A_Doc_Context _context) : IPatientRepository
{
    public async Task AddAsync(Patient patient, CancellationToken cancellationToken)
    {
        await _context.AddAsync(patient);
    }
}
