using Book_A_Doc.Application.Services;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.SuccesMessage;
using MediatR;

namespace Book_A_Doc.Application.Command.AuthCommands.ForgetPasswordCommand;

public class ForgetPasswordCommandHandler(IIdentityService identityService,IOtpService otpService
    ,IEmailTemplateService emailTemplateService,IBackgroundService backgroundService
    ,IEmailService emailService) : IRequestHandler<ForgetPasswordCommand, Result>
{
    public async Task<Result> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await identityService.FindByEmailAsync(request.Email);
        if (user == null) 
        { 
            return Result.Success(AuthMessages.OtpSent);
        }

        var cacheKey = $"forgot-password:{user.Email!}";

        var code = await otpService.GenerateAndStoreAsync(
            cacheKey,
            TimeSpan.FromMinutes(10));

        var emailBody = emailTemplateService.GenerateForgotPasswordTemplate(
    user.FullName,
    code);

        backgroundService.Enqueue(() => emailService.SendEmailAsync(
            user.Email!,
            "Reset Your Password",
            emailBody));

        return Result.Success(AuthMessages.OtpSent);

    }
}
