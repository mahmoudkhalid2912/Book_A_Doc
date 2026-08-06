using Book_A_Doc.Application.Services;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using Book_A_Doc.Domain.ResultPattern.SuccesMessage;
using MediatR;

namespace Book_A_Doc.Application.Command.AuthCommands.VerifyOtpCommand;

public class VerifyOtpCodeCommandHandler(IOtpService otpService) : IRequestHandler<VerifyOtpCommand, Result>
{
    public async Task<Result> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
    {
        var cacheKey = $"forgot-password:{request.Email}";

        var isOtpValid = await otpService.ValidateAsync(
            cacheKey,
            request.Code);

        if (!isOtpValid)
        {
            return Result.Failure(AuthErrors.InvalidOtp);
        }

        return Result.Success(AuthMessages.OtpVerified);
    }
}
