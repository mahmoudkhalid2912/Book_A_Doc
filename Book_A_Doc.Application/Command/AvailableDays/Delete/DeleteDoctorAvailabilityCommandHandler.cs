using Book_A_Doc.Domain.Repositories;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using MediatR;

namespace Book_A_Doc.Application.Command.AvailableDays.Delete;

public class DeleteDoctorAvailabilityCommandHandler(
    IDoctorAvailabilityRepository repository)
    : IRequestHandler<
        DeleteDoctorAvailabilityCommand,
        Result>
{
    public async Task<Result> Handle(
        DeleteDoctorAvailabilityCommand request,
        CancellationToken cancellationToken)
    {
        var availability =
            await repository.GetTrackedByIdAsync(
                request.Id,
                request.DoctorId,
                cancellationToken);

        if (availability is null)
        {
            return Result.Failure(
                DoctorAvailabilityErrors
                    .AvailabilityNotFound);
        }

        availability.IsDeleted = true;

        return Result.Success();
    }
}