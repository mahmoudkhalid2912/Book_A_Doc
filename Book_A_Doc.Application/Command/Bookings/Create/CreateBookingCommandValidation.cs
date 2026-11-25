using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using FluentValidation;

namespace Book_A_Doc.Application.Commands.Bookings.Create;

public class CreateBookingCommandValidation : AbstractValidator<CreateBookingCommand>
{
    public CreateBookingCommandValidation()
    {
        RuleFor(x => x.SlotId)
            .NotEmpty()
                .WithErrorCode(SlotsError.SlotIdRequired.Code)
                .WithMessage(SlotsError.SlotIdRequired.Description);

        RuleFor(x => x.PatientId)
            .NotEmpty()
                .WithErrorCode(BookingsError.PatientIdRequired.Code)
                .WithMessage(BookingsError.PatientIdRequired.Description);
    }
}
