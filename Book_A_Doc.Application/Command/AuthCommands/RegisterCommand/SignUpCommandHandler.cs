using Book_A_Doc.Application.Services;
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
        var emailExists = await identityService.EmailExistsAsync(request.Email);

        if (emailExists)
        {
            return Result.Failure(RegisterErrors.UserAlreadyExists);
        }

        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            FullName = request.FullName,
            BirthDate = request.BirthDay,
            PhoneNumber = request.PhoneNumber
        };

        var createResult = await identityService.CreateUserAsync(user, request.Password);

        if (createResult.IsFailure)
        {
            return createResult;
        }

        var token = await authenticationService.GenerateEmailConfirmationTokenAsync(user);
        token = tokenEncoder.Encode(token);

        var confirmationLink =
            $"{applicationSettings.BaseUrl}/api/Auth/ConfirmEmail" +
            $"?userId={user.Id}&token={token}";

        var emailBody = emailTemplateService.GenerateEmailConfirmationTemplate(
            user.FullName,
            confirmationLink);



         backgroundService.Enqueue(() => emailService.SendEmailAsync(
             user.Email!,
             AuthMessages.ConfirmationEmailSent,
             emailBody));
       
        return Result.Success(AuthMessages.RegisterSuccess);
    }
}