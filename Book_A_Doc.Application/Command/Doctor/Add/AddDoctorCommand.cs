using Book_A_Doc.Domain.ResultPattern;
using MediatR;

namespace Book_A_Doc.Application.Command.Doctor.Add;

public class AddDoctorCommand:IRequest<Result<Guid>>
{
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Specialty { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;

    public byte YearsOfExperience { get; set; }

    public decimal SessionPrice { get; set; }

    public DateOnly? BirthDate { get; set; }

    public string PhoneNumber { get; set; }= string.Empty;
}
