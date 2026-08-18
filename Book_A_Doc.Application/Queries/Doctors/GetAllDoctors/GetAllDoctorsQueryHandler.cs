using Book_A_Doc.Domain.Repositories;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.SuccessMessages;
using MediatR;

namespace Book_A_Doc.Application.Queries.Doctors.GetAllDoctors;

public class GetAllDoctorsQueryHandler(IDoctorRepository doctorRepository) : IRequestHandler<GetAllDoctorsQuery, Result<List<GetAllDoctorsResponse>>>
{
    public async Task<Result<List<GetAllDoctorsResponse>>> Handle(GetAllDoctorsQuery request, CancellationToken cancellationToken)
    {
        var doctors = await doctorRepository.GetAllDoctorsAsync(cancellationToken);

        var response = doctors.Select(d => new GetAllDoctorsResponse
        {
            Id = d.UserId,
            FullName = d.User.FullName,
            Specialty = d.Specialty,
            Description = d.Description,
            YearsOfExperience = d.YearsOfExperience,
            SessionPrice = d.SessionPrice,
            ProfileImageUrl = d.User.ProfileImageUrl
        }).ToList();

        return Result.Success(response, UserMessages.DoctorsRetrievedSuccessfully);
    }
}
