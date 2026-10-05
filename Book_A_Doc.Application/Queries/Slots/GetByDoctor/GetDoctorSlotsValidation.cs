using Book_A_Doc.Domain.ResultPattern.ErrorMessage;
using FluentValidation;

namespace Book_A_Doc.Application.Queries.Slots.GetByDoctor;

public class GetDoctorSlotsValidation : AbstractValidator<GetDoctorSlotsQuery>
{
    private const int MaxDateRangeInDays = 31;

    public GetDoctorSlotsValidation()
    {
        
        RuleFor(x => x.DoctorId)
            .NotEmpty()
                .WithErrorCode(SlotsError.DoctorIdRequired.Code)
                .WithMessage(SlotsError.DoctorIdRequired.Description);


        RuleFor(x => x.StartDate)
            .NotEmpty()
                ;


        RuleFor(x => x.EndDate)
            .NotEmpty()
            .GreaterThanOrEqualTo(x => x.StartDate)
                .WithErrorCode(SlotsError.InvalidDateRange.Code)
                .WithMessage(SlotsError.InvalidDateRange.Description);

       
        RuleFor(x => x)
            .Must(x => (x.EndDate.DayNumber - x.StartDate.DayNumber) <= MaxDateRangeInDays)
                .WithErrorCode(SlotsError.DateRangeTooLarge.Code)
                .WithMessage(SlotsError.DateRangeTooLarge.Description)
            .When(x => x.StartDate <= x.EndDate);  
    }
}