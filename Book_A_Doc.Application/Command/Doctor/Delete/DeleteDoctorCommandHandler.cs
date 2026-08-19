using Book_A_Doc.Application.Services;
using Book_A_Doc.Domain.Repositories;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using Book_A_Doc.Domain.ResultPattern.SuccessMessages;
using MediatR;

namespace Book_A_Doc.Application.Command.Doctor.Delete;

public class DeleteDoctorCommandHandler(IIdentityService identityService,IDoctorRepository doctorRepository) : IRequestHandler<DeleteDoctorCommand, Result>
{
    public async Task<Result> Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
    {
        var user = await identityService.FindByIdAsync(request.id);
        if (user is null)
        {
            return Result.Failure(UserErrors.UserNotFound);
        }

        var UserDeletedResult = await doctorRepository.DeleteAsync(request.id);
        if(UserDeletedResult.IsFailure)
        {
            return UserDeletedResult;
        }

        return Result.Success(UserMessages.DoctorDeletedSuccessfully);
    }
}
