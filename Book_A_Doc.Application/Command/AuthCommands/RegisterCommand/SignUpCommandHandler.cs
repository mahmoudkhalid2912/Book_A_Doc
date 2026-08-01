using Book_A_Doc.Domain.Models.Identity;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using Book_A_Doc.Domain.ResultPattern.SuccesMessage;
using Book_A_Doc.Infrastructre.JwtServices.OptionsClass;
using Book_A_Doc.Infrastructre.MailService;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text;

namespace Book_A_Doc.Application.Command.AuthCommands.RegisterCommand;

public class SignUpCommandHandler(UserManager<ApplicationUser> _userManager, IOptions<ApplicationSettings> appSettings,IEmailSender emailSender) : IRequestHandler<SignUpCommand, Result>
{
    public async Task<Result> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {

        var emailExists = await _userManager.Users
         .AnyAsync(x => x.Email == request.Email, cancellationToken);

        if (emailExists)
        {
            return Result.Failure(RegisterErrors.UserAlreadyExists);
        }

        // Create a new ApplicationUser object.
        var ApplicationUser = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            FullName = request.FullName,
            BirthDate = request.BirthDay,
            PhoneNumber = request.PhoneNumber
        };

        // Create the user using UserManager.CreateAsync().
        var createResult = await _userManager.CreateAsync(ApplicationUser, request.Password);

        // If creation fails, return the Identity errors.
        if (!createResult.Succeeded)
        {
            var error = createResult.Errors.First();
            return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(ApplicationUser);
        token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        var confirmationLink =$"{appSettings.Value.BaseUrl}/api/Auth/ConfirmEmail" 
            +$"?userId={ApplicationUser.Id}&token={token}";

        var emailBody = EmailBodyBuilder.GenerateEmailBody(
    "EmailConfirmation",
    new Dictionary<string, string>
         {
                 { "UserName", ApplicationUser.FullName },
                 { "ConfirmationLink", confirmationLink }
        });

        await emailSender.SendEmailAsync(ApplicationUser.Email, AuthMessages.ConfirmationEmailSent, emailBody);

        return Result.Success(AuthMessages.RegisterSuccess);
    }
}
