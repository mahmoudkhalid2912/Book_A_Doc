using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using FluentValidation;

namespace Book_A_Doc.Application.Commands.Slots.Activate;

public class ActivateSlotValidation : AbstractValidator<ActivateSlotCommand>
{
    public ActivateSlotValidation()
    {
        RuleFor(x => x.SlotId)
            .NotEmpty()
                .WithErrorCode(SlotsError.SlotIdRequired.Code)
                .WithMessage(SlotsError.SlotIdRequired.Description);
    }
}