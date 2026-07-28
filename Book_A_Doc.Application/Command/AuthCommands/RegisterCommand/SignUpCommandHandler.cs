using Book_A_Doc.Domain.Models.Identity;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using Book_A_Doc.Domain.ResultPattern.SuccesMessage;
using Book_A_Doc.Infrastructre.JwtServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Text;

namespace Book_A_Doc.Application.Command.AuthCommands.RegisterCommand;

public class SignUpCommandHandler(UserManager<ApplicationUser> _userManager) : IRequestHandler<SignUpCommand, Result>
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
        var createResult = await _userManager.CreateAsync(ApplicationUser,request.Password);

        // If creation fails, return the Identity errors.
        if (!createResult.Succeeded)
        {
            var error = createResult.Errors.First();
            return Result.Failure(new Error(error.Code,error.Description, StatusCodes.Status400BadRequest));
        }

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(ApplicationUser);
        token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        // Make Confirmation email latter


        // assign user as a patien first

        return Result.Success(AuthMessages.RegisterSuccess);
    }
}
