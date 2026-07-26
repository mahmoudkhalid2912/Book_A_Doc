using FluentValidation;

namespace Book_A_Doc.Application.Quiers.AuthQuery.RefreshTokenQuery;

public class RevokeRefreshTokenCommandValidator:AbstractValidator<RevokeRefreshTokenCommand>
{
    public RevokeRefreshTokenCommandValidator()
    {
        RuleFor(x => x.Token).NotEmpty();
        RuleFor(x=> x.RefreshToken).NotEmpty();
    }
}
