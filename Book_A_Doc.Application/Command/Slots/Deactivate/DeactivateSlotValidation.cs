using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using FluentValidation;

namespace Book_A_Doc.Application.Commands.Slots.Deactivate;

public class DeactivateSlotValidation : AbstractValidator<DeactivateSlotCommand>
{
    public DeactivateSlotValidation()
    {
        RuleFor(x => x.SlotId)
            .NotEmpty()
                .WithErrorCode(SlotsError.SlotIdRequired.Code)
                .WithMessage(SlotsError.SlotIdRequired.Description);
    }
}