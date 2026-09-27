using Book_A_Doc.Application.Interfaces;
using Book_A_Doc.Domain.ResultPattern;

namespace Book_A_Doc.Application.Command.Doctor.Update;

public class UpdateDoctorCommand:ITransactionalRequest<Result>
{
    public Guid Id { get; set; }
    public string? FullName { get; set; } = string.Empty;
    public string? Specialty { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public byte? YearsOfExperience { get; set; }
    public decimal? SessionPrice { get; set; }
    public DateOnly? BirthDate { get; set; }
    public string? PhoneNumber { get; set; } = string.Empty;
}
