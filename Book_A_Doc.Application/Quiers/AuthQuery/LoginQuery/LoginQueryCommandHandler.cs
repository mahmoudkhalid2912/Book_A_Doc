using Book_A_Doc.Application.Services.Jwt_Service;
using Book_A_Doc.Domain.Models.Identity;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using Book_A_Doc.Infrastructre.JwtServices;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Book_A_Doc.Application.Quiers.AuthQuery.LoginQuery;

public class LoginQueryCommandHandler(UserManager<ApplicationUser> userManager,IJwtProvider jwtProvider) : IRequestHandler<LoginQueryCommand, Result<LoginDtoResponse>>
{
    public async Task<Result<LoginDtoResponse>> Handle(LoginQueryCommand request, CancellationToken cancellationToken)
    {
        //Check if user exists
        var User = await userManager.FindByEmailAsync(request.Email);

        if (User == null) 
        { 
            return Result.Failure<LoginDtoResponse>(LoginErrors.InvalidCredentials);
        }

        //Check if password is correct
        bool isPasswordValid = await userManager.CheckPasswordAsync(User, request.Password);

        if (!isPasswordValid) return Result.Failure<LoginDtoResponse>(LoginErrors.InvalidCredentials);

        // Generate JWT Token
        var (token,expiresIn) = jwtProvider.GenerateJwtToken(User);

        // Return the response
        var LoginResponse = new LoginDtoResponse
        {
            UserId = User.Id,
            FullName = User.FullName,
            Email = User.Email!,
            Token = token,
            TokenExpireIn = expiresIn
        };
        return Result.Success(LoginResponse);
    }
}

