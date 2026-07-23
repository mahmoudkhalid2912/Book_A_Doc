using Book_A_Doc.Domain.Models.Identity;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Book_A_Doc.Application.Command.AuthCommands.RegisterCommand;

public class SignInCommandHandler(UserManager<ApplicationUser> _userManager) : IRequestHandler<SignInCommand, Result>
{
    public async Task<Result> Handle(SignInCommand request, CancellationToken cancellationToken)
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
            return Result.Failure(RegisterErrors.UserCreationFailed);
        }

        // Generate an email confirmation token.(To Do)

        // Send the confirmation email containing the confirmation link.(To Do)


        // Return a success response indicating that registration completed
        // and the user must confirm their email before signing in

        return Result.Success("Registration successful. Please check your email to confirm your account.");
    }
}
