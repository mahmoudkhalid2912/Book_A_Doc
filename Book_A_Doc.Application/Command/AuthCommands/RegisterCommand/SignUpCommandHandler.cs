using Book_A_Doc.Application.BackgroundJobs;
using Book_A_Doc.Application.Services;
using Book_A_Doc.Domain.Consts;
using Book_A_Doc.Domain.Models.Identity;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using Book_A_Doc.Domain.ResultPattern.SuccesMessage;
using MediatR;

namespace Book_A_Doc.Application.Command.AuthCommands.RegisterCommand;

public class SignUpCommandHandler(
    IIdentityService identityService,
    ITokenEncoder tokenEncoder,
    IApplicationSettings applicationSettings,
    IEmailTemplateService emailTemplateService,
    IAuthenticationService authenticationService,
    IEmailService emailService,
    IBackgroundService backgroundService)
    : IRequestHandler<SignUpCommand, Result>
{
    public async Task<Result> Handle(
        SignUpCommand request,
        CancellationToken cancellationToken)
    {
        var emailExists =
            await identityService.EmailExistsAsync(request.Email);

        if (emailExists)
        {
            return Result.Failure(
                AuthErrors.UserAlreadyExists);
        }

        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            FullName = request.FullName,
            BirthDate = request.BirthDay,
            PhoneNumber = request.PhoneNumber
        };

        // Create User + Assign Patient Role inside transaction
        var createResult =
            await identityService.CreateUserWithRoleAsync(
                user,
                request.Password,
                DefaultRoles.Patient);

        if (createResult.IsFailure)
        {
            return createResult;
        }

        // Generate email confirmation token
        var token =
            await authenticationService
                .GenerateEmailConfirmationTokenAsync(user);

        token = tokenEncoder.Encode(token);

        // Generate confirmation link
        var confirmationLink =
            $"{applicationSettings.BaseUrl}/api/Auth/ConfirmEmail" +
            $"?userId={user.Id}&token={token}";

        // Generate email body
        var emailBody =
            emailTemplateService.GenerateEmailConfirmationTemplate(
                user.FullName,
                confirmationLink);

        // Send email as background job
        backgroundService.Enqueue(() =>
            emailService.SendEmailAsync(
                user.Email!,
                AuthMessages.ConfirmationEmailSent,
                emailBody));

        backgroundService.Enqueue<CreatePatientJob>(
             job => job.ExecuteAsync(user.Id));

        return Result.Success(
            AuthMessages.RegisterSuccess);
    }
}