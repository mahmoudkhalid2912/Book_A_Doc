using Book_A_Doc.Application.Services;
using Book_A_Doc.Domain.Repositories;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using Book_A_Doc.Domain.ResultPattern.SuccessMessages;
using MediatR;

namespace Book_A_Doc.Application.Command.Doctor.Update;

public class UpdateDoctorCommandHandler(
    IIdentityService identityService,
    IDoctorRepository doctorRepository)
    : IRequestHandler<UpdateDoctorCommand, Result>
{
    public async Task<Result> Handle(
        UpdateDoctorCommand request,
        CancellationToken cancellationToken)
    {
        var user = await identityService.FindByIdAsync(request.Id);

        if (user is null)
        {
            return Result.Failure(
                UserErrors.UserNotFound);
        }

        var updateResult = await doctorRepository.UpdateAsync(
            request.Id,
            request.FullName,
            request.Specialty,
            request.Description,
            request.YearsOfExperience,
            request.SessionPrice,
            request.BirthDate,
            request.PhoneNumber);

        if (updateResult.IsFailure)
        {
            return updateResult;
        }

        return Result.Success(
            UserMessages.DoctorUpdatedSuccessfully,UserMessages.DoctorUpdatedSuccessfully);
    }
}