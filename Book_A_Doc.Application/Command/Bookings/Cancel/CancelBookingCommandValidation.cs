using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using FluentValidation;

namespace Book_A_Doc.Application.Commands.Bookings.Cancel;

public class CancelBookingCommandValidation : AbstractValidator<CancelBookingCommand>
{
    public CancelBookingCommandValidation()
    {
        RuleFor(x => x.BookingId)
            .NotEmpty()
                .WithErrorCode(BookingsError.BookingIdRequired.Code)
                .WithMessage(BookingsError.BookingIdRequired.Description);

        RuleFor(x => x.PatientId)
            .NotEmpty()
                .WithErrorCode(BookingsError.PatientIdRequired.Code)
                .WithMessage(BookingsError.PatientIdRequired.Description);
    }
}
