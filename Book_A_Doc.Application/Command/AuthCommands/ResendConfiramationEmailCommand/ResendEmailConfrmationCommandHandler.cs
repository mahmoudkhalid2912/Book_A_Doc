using Book_A_Doc.Application.Services;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using Book_A_Doc.Domain.ResultPattern.SuccesMessage;
using MediatR;

namespace Book_A_Doc.Application.Command.AuthCommands.ResendConfiramationEmailCommand;

public class ResendEmailConfrmationCommandHandler(
    IIdentityService identityService,
    ITokenEncoder tokenEncoder,
    IApplicationSettings applicationSettings,
    IEmailTemplateService emailTemplateService,
    IEmailService emailService,
    IAuthenticationService authenticationService)
    : IRequestHandler<ResendEmailConfiramtionCommand, Result>
{
    public async Task<Result> Handle(
        ResendEmailConfiramtionCommand request,
        CancellationToken cancellationToken)
    {
        var user = await identityService.FindByEmailAsync(request.Email);

        if (user is null)
        {
            // Don't reveal whether the email exists.
            return Result.Success(AuthMessages.ConfirmationEmailSent);
        }

        if (user.EmailConfirmed)
        {
            return Result.Failure(EmailConfirmationError.DuplicatedConfirmation);
        }

        var token = await authenticationService.GenerateEmailConfirmationTokenAsync(user);
        token = tokenEncoder.Encode(token);

        var confirmationLink =
            $"{applicationSettings.BaseUrl}/api/Auth/ConfirmEmail" +
            $"?userId={user.Id}&token={token}";

        var emailBody = emailTemplateService.GenerateEmailConfirmationTemplate(
            user.FullName,
            confirmationLink);

        await emailService.SendEmailAsync(
            user.Email!,
            AuthMessages.ConfirmationEmailSent,
            emailBody);

        return Result.Success(AuthMessages.ConfirmationEmailSent);
    }
}