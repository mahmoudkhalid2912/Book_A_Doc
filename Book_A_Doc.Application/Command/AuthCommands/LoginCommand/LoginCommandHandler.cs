using Book_A_Doc.Application.Command.AuthCommands.LoginCommand;
using Book_A_Doc.Domain.Models.Identity;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using Book_A_Doc.Domain.ResultPattern.SuccesMessage;
using Book_A_Doc.Infrastructre.JwtServices;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Book_A_Doc.Application.Command.AuthCommands.LoginCommand;

public class LoginCommandHandler(UserManager<ApplicationUser> userManager,IJwtProvider jwtProvider,SignInManager<ApplicationUser>signInManager) : IRequestHandler<LoginCommand, Result<LoginResponse>>
{
    private readonly int RefreshTokenExpiryDays=30;
    public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        //Check if user exists
        var User = await userManager.FindByEmailAsync(request.Email);

        if (User == null) 
        { 
            return Result.Failure<LoginResponse>(LoginErrors.InvalidCredentials);
        }


        // check password using SignInManager
        var result = await signInManager.PasswordSignInAsync(User, request.Password, false,false);

        if (result.Succeeded) 
        {
            // Generate JWT Token
            var (token, expiresIn) = jwtProvider.GenerateJwtToken(User);



            // Generate Refresh Token
            var refreshToken = jwtProvider.GenerateRefreshToken();
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(RefreshTokenExpiryDays);

            // Save the refresh token and its expiration to the user entity
            User.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                ExpiresOn = refreshTokenExpiration
            });
            await userManager.UpdateAsync(User);


            // Return the response
            var LoginResponse = new LoginResponse
            {
                UserId = User.Id,
                FullName = User.FullName,
                Email = User.Email!,
                Token = token,
                TokenExpireIn = expiresIn,
                RefreshToken = refreshToken,
                RefreshTokenExpiration = refreshTokenExpiration,
            };
            return Result.Success(LoginResponse, AuthMessages.LoginSuccess);
        }

        // Password is incorrect or user doesn't confirm there email

        return Result.Failure<LoginResponse>
            (result.IsNotAllowed?
            LoginErrors.EmailNotConfirmed
            :LoginErrors.InvalidCredentials);




    }

    
}

