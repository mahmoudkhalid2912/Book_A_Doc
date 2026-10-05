using Book_A_Doc.Domain.Interfaces.Repositories;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using Book_A_Doc.Domain.ResultPattern.SuccessMessages;
using MediatR;

namespace Book_A_Doc.Application.Commands.Slots.Activate;

public class ActivateSlotCommandHandler(ISlotRepository repository)
    : IRequestHandler<ActivateSlotCommand, Result>
{
    public async Task<Result> Handle(
        ActivateSlotCommand request,
        CancellationToken cancellationToken)
    {
        
        var slot = await repository.GetByIdAsync(request.SlotId, cancellationToken);

        
        if (slot is null)
        {
            return Result.Failure(SlotsError.SlotNotFound);
        }

        
        if (slot.IsActive)
        {
            return Result.Failure(SlotsError.SlotAlreadyActive);
        }

        if (slot.Booking is not null
           && (slot.Booking.Status == BookingStatus.PendingPayment
             || slot.Booking.Status == BookingStatus.Confirmed))
        {
            return Result.Failure(SlotsError.SlotAlreadyBooked);
        }

        slot.Activate();

        
        repository.Update(slot);

        return Result.Success(SlotsMessages.SlotActivated);
    }
}