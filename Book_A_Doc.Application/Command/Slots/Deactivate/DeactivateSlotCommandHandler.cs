using Book_A_Doc.Domain.Interfaces.Repositories;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using Book_A_Doc.Domain.ResultPattern.SuccessMessages;
using MediatR;

namespace Book_A_Doc.Application.Commands.Slots.Deactivate;

public class DeactivateSlotCommandHandler(ISlotRepository repository)
    : IRequestHandler<DeactivateSlotCommand, Result>
{
    public async Task<Result> Handle(
        DeactivateSlotCommand request,
        CancellationToken cancellationToken)
    {
       
        var slot = await repository.GetByIdAsync(request.SlotId, cancellationToken);

        
        if (slot is null)
        {
            return Result.Failure(SlotsError.SlotNotFound);
        }

        
        if (!slot.IsActive)
        {
            return Result.Failure(SlotsError.SlotAlreadyDeactivated);
        }

        
        if (slot.Booking is not null
            && (slot.Booking.Status == BookingStatus.PendingPayment
             || slot.Booking.Status == BookingStatus.Confirmed))
        {
            return Result.Failure(SlotsError.SlotAlreadyBooked);
        }

        
        slot.Deactivate();

       
        repository.Update(slot);

        return Result.Success(SlotsMessages.SlotDeactivated);
    }
}