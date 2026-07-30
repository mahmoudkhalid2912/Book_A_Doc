using Book_A_Doc.Domain.Models.Identity;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using Book_A_Doc.Domain.ResultPattern.SuccesMessage;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace Book_A_Doc.Application.Command.AuthCommands.ResendConfiramationEmailCommand;

public class ResendEmailConfrmationCommandHandler(UserManager<ApplicationUser>userManager) : IRequestHandler<ResendEmailConfiramtionCommand, Result>
{
    public async Task<Result> Handle(ResendEmailConfiramtionCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return Result.Success(AuthMessages.ConfirmationEmailSent);
        }

        if (user.EmailConfirmed)
        {
            return Result.Failure(EmailConfirmationError.DuplicatedConfirmation);
        }

        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
        token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        return Result.Success(AuthMessages.ConfirmationEmailSent);
    }
}
