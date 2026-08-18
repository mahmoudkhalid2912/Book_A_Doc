using Book_A_Doc.Domain.Repositories;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using Book_A_Doc.Domain.ResultPattern.SuccessMessages;
using MediatR;

namespace Book_A_Doc.Application.Queries.Doctors.GetDoctor;

public class GetDoctorQueryHandler(IDoctorRepository doctorRepository) : IRequestHandler<GetDoctorQuery, Result<GetDoctorResponse>>
{
    public async Task<Result<GetDoctorResponse>> Handle(GetDoctorQuery request, CancellationToken cancellationToken)
    {
        var doctor = await doctorRepository.GetDoctorAsync(request.Id, cancellationToken);
        if (doctor == null)
        {
            return Result.Failure<GetDoctorResponse>(UserErrors.DoctorNotFound);
        }

        var response = new GetDoctorResponse
        {
            Id = doctor.UserId,
            FullName = doctor.FullName,
            Specialty = doctor.Specialty,
            Description = doctor.Description,
            YearsOfExperience = doctor.YearsOfExperience,
            SessionPrice = doctor.SessionPrice,
            ProfileImageUrl = doctor.User.ProfileImageUrl
        };

        return Result.Success(response, UserMessages.DoctorRetrievedSuccessfully);
    }
}
