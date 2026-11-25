using Book_A_Doc.Domain.Interfaces.Repositories;
using Book_A_Doc.Domain.Repositories;
using Book_A_Doc.Domain.ResultPattern;
using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using Book_A_Doc.Domain.ResultPattern.SuccessMessages;
using MediatR;

namespace Book_A_Doc.Application.Commands.Bookings.Create;

public class CreateBookingCommandHandler(
    ISlotRepository slotRepository,
    IBookingRepository bookingRepository,
    IDoctorRepository doctorRepository,
    IPatientRepository patientRepository)
    : IRequestHandler<CreateBookingCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(
        CreateBookingCommand request,
        CancellationToken cancellationToken)
    {
        var slot = await slotRepository.GetByIdAsync(request.SlotId, cancellationToken);

        if (slot is null)
        {
            return Result.Failure<Guid>(SlotsError.SlotNotFound);
        }

        if (!slot.IsActive)
        {
            return Result.Failure<Guid>(SlotsError.SlotNotAvailableForBooking);
        }

        if (HasStarted(slot))
        {
            return Result.Failure<Guid>(BookingsError.SlotInPast);
        }

        if (slot.Booking is not null
            && (slot.Booking.Status == BookingStatus.PendingPayment
                || slot.Booking.Status == BookingStatus.Confirmed))
        {
            return Result.Failure<Guid>(SlotsError.SlotAlreadyBooked);
        }

        if (!await patientRepository.ExistsAsync(request.PatientId, cancellationToken))
        {
            return Result.Failure<Guid>(BookingsError.PatientNotFound);
        }

        var doctor = await doctorRepository.GetDoctorAsync(slot.DoctorId, cancellationToken);

        if (doctor is null)
        {
            return Result.Failure<Guid>(SlotsError.DoctorNotFound);
        }

        // A slot has at most one booking row. If an earlier booking was
        // cancelled or expired, reuse that row for the new patient.
        if (slot.Booking is not null)
        {
            var booking = slot.Booking;

            booking.PatientId = request.PatientId;
            booking.Status = BookingStatus.PendingPayment;
            booking.Amount = doctor.SessionPrice;
            booking.UpdatedOn = DateTime.UtcNow;

            bookingRepository.Update(booking);

            return Result.Success(booking.Id, BookingsMessages.BookingCreated);
        }

        var newBooking = new Booking
        {
            Id = Guid.NewGuid(),
            PatientId = request.PatientId,
            DoctorId = slot.DoctorId,
            AvailabilitySlotId = slot.Id,
            Status = BookingStatus.PendingPayment,
            Amount = doctor.SessionPrice,
            CreatedOn = DateTime.UtcNow
        };

        await bookingRepository.AddAsync(newBooking, cancellationToken);

        return Result.Success(newBooking.Id, BookingsMessages.BookingCreated);
    }

    private static bool HasStarted(AvailabilitySlot slot)
    {
        var nowInEgypt = TimeZoneInfo.ConvertTimeFromUtc(
            DateTime.UtcNow,
            GetEgyptTimeZone());

        var slotStart = slot.Date.ToDateTime(slot.StartTime);

        return slotStart <= nowInEgypt;
    }

    private static TimeZoneInfo GetEgyptTimeZone()
    {
        try
        {
            return TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
        }
        catch (TimeZoneNotFoundException)
        {
            return TimeZoneInfo.FindSystemTimeZoneById("Africa/Cairo");
        }
    }
}
