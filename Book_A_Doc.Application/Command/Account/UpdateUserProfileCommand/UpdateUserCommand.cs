using Book_A_Doc.Domain.ResultPattern;
using MediatR;
using System.Text.Json.Serialization;

namespace Book_A_Doc.Application.Command.Account.UpdateUserProfileCommand;

public  class UpdateUserCommand : IRequest<Result<bool>>
{
    [JsonIgnore]
    public Guid UserId { get; set; }

    public string Name { get; set; }

    public DateOnly? BirthDate { get; set; }

    public string? Phone { get; set; }
}

