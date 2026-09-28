using Book_A_Doc.Domain.ResultPattern;
using MediatR;

namespace Book_A_Doc.Application.Queries.Slots.GetAll;

public class GetAvailableSlotsQuery:IRequest<Result<IEnumerable<AvailableSlotsDtoResponse>>>
{
    public Guid DoctorId { get; set; }

    public DateOnly Date { get; set; }
}
