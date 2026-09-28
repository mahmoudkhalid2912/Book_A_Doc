using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using FluentValidation;

namespace Book_A_Doc.Application.Queries.Slots.GetAll;

public class GetAvailibleSlotsValidation : AbstractValidator<GetAvailableSlotsQuery>
{
    public GetAvailibleSlotsValidation()
    {
        
        RuleFor(x => x.DoctorId)
            .NotEmpty().WithMessage(SlotsError.DoctorIdRequired.Description);

        RuleFor(x => x.Date)
            .NotEmpty()
            .Must(BeNotBeforeTodayInEgypt)
            .WithMessage(SlotsError.DateInPast.Description);
    }

    
    private bool BeNotBeforeTodayInEgypt(DateOnly date)
    {
        var egyptTimeZone = GetEgyptTimeZone();
        var nowInEgypt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, egyptTimeZone);
        var todayInEgypt = DateOnly.FromDateTime(nowInEgypt);

        return date >= todayInEgypt;
    }

   
    private TimeZoneInfo GetEgyptTimeZone()
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