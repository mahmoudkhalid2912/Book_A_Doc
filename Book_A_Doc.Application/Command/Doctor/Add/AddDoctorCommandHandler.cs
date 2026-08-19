using Book_A_Doc.Application.Services;
using Book_A_Doc.Domain.Consts;
using Book_A_Doc.Domain.Models.Identity;
using DoctorModel = Book_A_Doc.Domain.Models.Identity.Doctor;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using Book_A_Doc.Domain.ResultPattern.SuccessMessages;
using MediatR;

namespace Book_A_Doc.Application.Command.Doctor.Add;

public class AddDoctorCommandHandler(
    IIdentityService identityService)
    : IRequestHandler<AddDoctorCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(
        AddDoctorCommand request,
        CancellationToken cancellationToken)
    {
        var emailExists =
            await identityService.EmailExistsAsync(request.Email);

        if (emailExists)
        {
            return Result.Failure<Guid>(
                AuthErrors.UserAlreadyExists);
        }

        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            FullName = request.FullName,
            BirthDate = request.BirthDate,
            PhoneNumber = request.PhoneNumber,
            EmailConfirmed = true,

            Doctor = new DoctorModel
            {
                Specialty = request.Specialty,
                Description = request.Description,
                YearsOfExperience = request.YearsOfExperience,
                SessionPrice = request.SessionPrice,
                FullName = request.FullName
            }
        };

        var createResult =
            await identityService.CreateUserWithRoleAsync(
                user,
                request.Password,
                DefaultRoles.Doctor);

        if (createResult.IsFailure)
        {
            return Result.Failure<Guid>(
                createResult.Error);
        }

        return Result.Success(
            user.Id,
            UserMessages.DoctorCreatedSuccessfully);
    }
}