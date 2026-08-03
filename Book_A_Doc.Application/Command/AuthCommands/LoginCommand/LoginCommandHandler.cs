using Book_A_Doc.Application.Services;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using Book_A_Doc.Domain.ResultPattern.SuccesMessage;
using MediatR;

namespace Book_A_Doc.Application.Command.AuthCommands.LoginCommand;

public class LoginCommandHandler(IIdentityService identityService,IJwtProvider jwtProvider,IAuthenticationService authenticationService,IRefreshTokenService refreshTokenService) : IRequestHandler<LoginCommand, Result<LoginResponse>>
{
    private readonly int RefreshTokenExpiryDays=30;
    public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        //Check if user exists
        var User = await identityService.FindByEmailAsync(request.Email);

        if (User == null) 
        { 
            return Result.Failure<LoginResponse>(LoginErrors.InvalidCredentials);
        }


        // check password using SignInManager
        var result = await  authenticationService.PasswordSignInAsync(User, request.Password);

        if (result.IsFailure) 
        {
            return Result.Failure<LoginResponse>(result.Error);
        }

        // Generate JWT Token
        var (token, expiresIn) = jwtProvider.GenerateJwtToken(User);
        // Generate Refresh Token
        var refreshToken = jwtProvider.GenerateRefreshToken();
        var refreshTokenExpiration = DateTime.UtcNow.AddDays(RefreshTokenExpiryDays);

        var addrefreshtokenresutl = await refreshTokenService.AddRefreshTokenAsync(User, refreshToken, refreshTokenExpiration);
        if(addrefreshtokenresutl.IsFailure)
        {
            return Result.Failure<LoginResponse>(addrefreshtokenresutl.Error);
        }
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

    
}

