using Book_A_Doc.Application.Interfaces;
using Book_A_Doc.Domain.ResultPattern;

namespace Book_A_Doc.Application.Commands.Slots.Deactivate;

public record DeactivateSlotCommand(Guid SlotId)
    : ITransactionalRequest<Result>;