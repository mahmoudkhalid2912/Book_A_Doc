using Book_A_Doc.Application.Services;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using Book_A_Doc.Domain.ResultPattern.SuccesMessage;
using MediatR;

namespace Book_A_Doc.Application.Command.AuthCommands.ResetPasswordCommand;

public class ResetPasswordCommandHandler(
    IOtpService otpService,
    IIdentityService identityService)
    : IRequestHandler<ResetPasswordCommand, Result>
{
    public async Task<Result> Handle(
        ResetPasswordCommand request,
        CancellationToken cancellationToken)
    {
        // 1- Validate OTP
        var cacheKey = $"forgot-password:{request.Email}";

        var isOtpValid = await otpService.ValidateAsync(
            cacheKey,
            request.Code);

        if (!isOtpValid)
        {
            return Result.Failure(
                EmailConfirmationError.InvalidOtp);
        }


        // 2- Reset Password
        var result = await identityService.ResetPasswordAsync(
            request.Email,
            request.NewPassword);


        if (!result.IsSuccess)
        {
            return result;
        }


        // 3- Remove OTP after success
        await otpService.RemoveAsync(cacheKey);


        return Result.Success(
            AuthMessages.PasswordResetSuccess);
    }
}