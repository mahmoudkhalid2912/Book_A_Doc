using Book_A_Doc.Domain.ResultPattern;
using MediatR;

namespace Book_A_Doc.Application.Queries.Slots.GetById;

public record GetSlotByIdQuery(Guid SlotId)
    : IRequest<Result<SlotDtoResponse>>;