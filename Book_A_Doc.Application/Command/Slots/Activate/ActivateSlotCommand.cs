using Book_A_Doc.Application.Interfaces;
using Book_A_Doc.Domain.ResultPattern;

namespace Book_A_Doc.Application.Commands.Slots.Activate;

public record ActivateSlotCommand(Guid SlotId)
    : ITransactionalRequest<Result>;