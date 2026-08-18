namespace Book_A_Doc.Application.Queries.Doctors.GetDoctor;

public class GetDoctorResponse
{
    public Guid Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Specialty { get; set; } = string.Empty;

    public string? Description { get; set; }

    public byte YearsOfExperience { get; set; }

    public decimal SessionPrice { get; set; }

    public string? ProfileImageUrl { get; set; }
}
